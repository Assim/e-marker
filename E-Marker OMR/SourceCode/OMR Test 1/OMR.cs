using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Math;
using AForge.Math.Geometry;
using System.IO;
using System.Xml;
using OMR;
using OMR.XML;
using OMR.Forms;
using System.Diagnostics;

namespace OMRReader_test1
{
    public class OMR
    {
        System.Drawing.Image scannedImage;

        public void action(string teacher, string student, string imagePath)
        {
            /// Get scan image and resize it
            scannedImage = System.Drawing.Image.FromFile(imagePath);
            scannedImage = (System.Drawing.Image)ImageUtilities.ResizeImage((Bitmap)scannedImage, 2100, 2100 * scannedImage.Height / scannedImage.Width);
            Bitmap unf = new Bitmap(scannedImage);
            OpticalReader reader = new OpticalReader();
            scannedImage = (System.Drawing.Image)reader.ExtractOMRSheet(unf, "sheets.xml", OMREnums.OMRSheet.A550);

            /// Get user selected options
            Rectangle[] Blocks = new Rectangle[]
            {
                OMRSheetReader.GetSheetPropertyLocation("sheets", OMREnums.OMRSheet.A550, OMREnums.OMRProperty.tensBlock1),
                OMRSheetReader.GetSheetPropertyLocation("sheets", OMREnums.OMRSheet.A550, OMREnums.OMRProperty.tensBlock2),
                OMRSheetReader.GetSheetPropertyLocation("sheets", OMREnums.OMRSheet.A550, OMREnums.OMRProperty.tensBlock3),
                OMRSheetReader.GetSheetPropertyLocation("sheets", OMREnums.OMRSheet.A550, OMREnums.OMRProperty.tensBlock4)
            };

            List<Bitmap[]> bmps = new List<Bitmap[]>();
            for (int i = 0; i < 4; i++)
            {
                bmps.Add(SliceOMarkBlock(scannedImage, Blocks[i], 10));
            }

            string ans = "";
            foreach (Bitmap[] blk in bmps)
            {
                foreach (Bitmap line in blk)
                {
                    ans += rateSlice(line, 5) + ",";
                }
            }
            Console.WriteLine(ans);
            
            /// Get exam ID
            OpticalReader rr = new OpticalReader();
            int examId = Convert.ToInt32(rr.getRegNumOfSheet(scannedImage, OMREnums.OMRSheet.A550, "sheets.xml", false).ToString());
            Console.WriteLine(examId);
            Console.Read();

            /// Return to Java application
            string jarPath = "E-Marker.jar";
            string argumentsFortheJarFile = "\"" + teacher + "\" \"" + student + "\" " + examId + " " + ans;
            System.Diagnostics.Process clientProcess = new Process();
            clientProcess.StartInfo.FileName = "java";
            clientProcess.StartInfo.Arguments = @"-jar "+ jarPath +" " + argumentsFortheJarFile;
            clientProcess.Start();
            clientProcess.WaitForExit();
        }

        private Bitmap ExtractOMRSheet(Bitmap basicImage, int fillint, int contint, string OMRSpecsSheetAddress)
        {
            scannedImage = (System.Drawing.Image)flatten(basicImage, fillint, contint);
            Application.DoEvents();
            //return ExtractPaperFromFlattened(new Bitmap(scannedImage), basicImage, minblb_tb.Value, fillint, contint, OMRSpecsSheetAddress);
            return ExtractPaperFromFlattened(new Bitmap(scannedImage), basicImage, 0, fillint, contint, OMRSpecsSheetAddress);
        }
        public Bitmap flatten(Bitmap bmp, int fillint, int contint)
        {
            // step 1 - turn background to black
            ColorFiltering colorFilter = new ColorFiltering();

            colorFilter.Red = new IntRange(0, fillint);
            colorFilter.Green = new IntRange(0, fillint);
            colorFilter.Blue = new IntRange(0, fillint);
            colorFilter.FillOutsideRange = false;

            colorFilter.ApplyInPlace(bmp);
            AForge.Imaging.Filters.ContrastCorrection Contrast = new ContrastCorrection(contint);
            AForge.Imaging.Filters.Invert invert = new Invert();
            AForge.Imaging.Filters.ExtractChannel extract_channel = new ExtractChannel(0);
            //AForge.Imaging.Filters.Threshold thresh_hold = new Threshold(thresh_tb.Value);
            AForge.Imaging.Filters.Threshold thresh_hold = new Threshold(0);
            bmp = invert.Apply(thresh_hold.Apply(extract_channel.Apply(Contrast.Apply(bmp))));

            return bmp;
        }
        private bool isSame(UnmanagedImage img1, UnmanagedImage img2)
        {
            int count = 0, tcount = img2.Width * img2.Height;
            for (int y = 0; y < img1.Height; y++)
                for (int x = 0; x < img1.Width; x++)
                {
                    Color c1 = img1.GetPixel(x, y), c2 = img2.GetPixel(x, y);
                    if ((c1.R + c1.G + c1.B) / 3 > (c2.R + c2.G + c2.B) / 3 - 10 &&
                        (c1.R + c1.G + c1.B) / 3 < (c2.R + c2.G + c2.B) / 3 + 10)
                        count++;
                }
            //return (count * 100) / tcount >= sim_tb.Value;
            return (count * 100) / tcount >= 1;
        }
        private Bitmap ExtractPaperFromFlattened(Bitmap bitmap, Bitmap basicImage, int minBlobWidHei, int fillint, int contint, string OMRSheets)
        {
            BitmapData bitmapData = bitmap.LockBits(
                new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadWrite, bitmap.PixelFormat);

            // lock image

            // step 2 - locating objects
            BlobCounter blobCounter = new BlobCounter();

            blobCounter.FilterBlobs = true;
            blobCounter.MinHeight = minBlobWidHei;
            blobCounter.MinWidth = minBlobWidHei;

            blobCounter.ProcessImage(bitmapData);
            Blob[] blobs = blobCounter.GetObjectsInformation();
            bitmap.UnlockBits(bitmapData);

            Graphics g = Graphics.FromImage(bitmap);
            Pen yellowPen = new Pen(Color.Yellow, 2); // circles
            Pen redPen = new Pen(Color.Red, 2);       // quadrilateral
            Pen brownPen = new Pen(Color.Brown, 2);   // quadrilateral with known sub-type
            Pen greenPen = new Pen(Color.Green, 2);   // known triangle
            Pen bluePen = new Pen(Color.Blue, 2);     // triangle

            Rectangle[] rects = blobCounter.GetObjectsRectangles();
            Blob[] blobs2 = blobCounter.GetObjects(bitmap, false);


            System.Drawing.Image compImg = System.Drawing.Image.FromFile("lc.jpg");
            UnmanagedImage compUMImg = UnmanagedImage.FromManagedImage((Bitmap)compImg);

            List<IntPoint> quad = new List<IntPoint>();
            try
            {
                //g.DrawRectangles(yellowPen, rects);
                foreach (Blob blob in blobs2)
                {
                    if (
                        ((double)blob.Area) / ((double)bitmap.Width * bitmap.Height) > 0.0001 &&
                        ((double)blob.Area) / ((double)bitmap.Width * bitmap.Height) < 0.005 &&
                            blob.Rectangle.X < (bitmap.Width) / 4)
                    {
                        if ((double)blob.Rectangle.Width / blob.Rectangle.Height < 1.4 &&
                            (double)blob.Rectangle.Width / blob.Rectangle.Height > .6)
                        {
                            compUMImg = UnmanagedImage.FromManagedImage(ImageUtilities.ResizeImage(compImg, blob.Rectangle.Width, blob.Rectangle.Height));
                            if (isSame(blob.Image, compUMImg))
                            {
                                g.DrawRectangle(yellowPen, blob.Rectangle);
                                quad.Add(new IntPoint((int)blob.CenterOfGravity.X, (int)blob.CenterOfGravity.Y));
                            }
                        }
                    }
                }
            }
            catch (ArgumentException) { MessageBox.Show("No Blobs"); }
            try
            {
                if (quad[0].Y > quad[1].Y)
                {
                    IntPoint tp = quad[0];
                    quad[0] = quad[1];
                    quad[1] = tp;
                }
            }
            catch
            {
            }
            compImg = System.Drawing.Image.FromFile("rc.jpg");
            compUMImg = UnmanagedImage.FromManagedImage((Bitmap)compImg);
            try
            {
                //g.DrawRectangles(yellowPen, rects);
                foreach (Blob blob in blobs2)
                {
                    if (
                        ((double)blob.Area) / ((double)bitmap.Width * bitmap.Height) > 0.0001 &&
                        ((double)blob.Area) / ((double)bitmap.Width * bitmap.Height) < 0.004 &&
                        blob.Rectangle.X > (bitmap.Width * 3) / 4)
                    {
                        if ((double)blob.Rectangle.Width / blob.Rectangle.Height < 1.4 &&
                        (double)blob.Rectangle.Width / blob.Rectangle.Height > .6)
                        {
                            compUMImg = UnmanagedImage.FromManagedImage(ImageUtilities.ResizeImage(compImg, blob.Rectangle.Width, blob.Rectangle.Height));
                            if (isSame(blob.Image, compUMImg))
                            {
                                g.DrawRectangle(yellowPen, blob.Rectangle);
                                quad.Add(new IntPoint((int)blob.CenterOfGravity.X, (int)blob.CenterOfGravity.Y));
                            }
                        }
                    }
                }
            }
            catch (ArgumentException) { MessageBox.Show("No Blobs"); }
            try
            {
                if (quad[2].Y < quad[3].Y)
                {
                    IntPoint tp = quad[2];
                    quad[2] = quad[3];
                    quad[3] = tp;
                }
            }
            catch
            {

            }
            yellowPen.Dispose();
            redPen.Dispose();
            greenPen.Dispose();
            bluePen.Dispose();
            brownPen.Dispose();
            g.Dispose();

            //// put new image to clipboard
            //Clipboard.SetDataObject(bitmap);
            // and to picture box
            if (quad.Count == 4)
            {
                if (((double)quad[1].Y - (double)quad[0].Y) / ((double)quad[2].Y - (double)quad[3].Y) < .75 ||
                    ((double)quad[1].Y - (double)quad[0].Y) / ((double)quad[2].Y - (double)quad[3].Y) > 1.25)
                    quad.Clear();
                else if (quad[0].X > bitmap.Width / 2 || quad[1].X > bitmap.Width / 2 || quad[2].X < bitmap.Width / 2 || quad[3].X < bitmap.Width / 2)
                    quad.Clear();
            }
            if (quad.Count != 4)
            {
                if (contint <= 60)
                {
                    if (contint >= 0)
                    {
                        contint += 5;
                        contint *= -1;
                        return ExtractOMRSheet(basicImage, fillint, contint, OMRSheets);
                    }
                    else
                    {
                        contint *= -1;
                        contint += 10;
                        return ExtractOMRSheet(basicImage, fillint, contint, OMRSheets);
                    }
                }
                else
                {
                    MessageBox.Show("Extraction Failed.");
                    return basicImage;
                }
            }
            else
            {
                IntPoint tp2 = quad[3];
                quad[3] = quad[1];
                quad[1] = tp2;
                QuadrilateralTransformation wrap = new QuadrilateralTransformation(quad);
                wrap.UseInterpolation = false;
                Rectangle sr = OMRSheetReader.GetSheetPropertyLocation(OMRSheets, OMREnums.OMRSheet.A550, OMREnums.OMRProperty.SheetSize);
                wrap.AutomaticSizeCalculaton = false;
                wrap.NewWidth = sr.Width;
                wrap.NewHeight = sr.Height;
                wrap.Apply(basicImage).Save("LastImg.jpg", ImageFormat.Jpeg);
                System.Drawing.Image imgl = (System.Drawing.Image)wrap.Apply(basicImage);
                Graphics gg = Graphics.FromImage(imgl);
                Pen pr = new Pen(Brushes.Red, 2);

                //gg.DrawRectangle(pr, MyXML.OMRSheetReader.GetSheetProperty(OMRSheets, MyXML.OMREnums.OMRSheet.A550, MyXML.OMREnums.OMRProperty.tensBlock1));
                //gg.DrawRectangle(pr, MyXML.OMRSheetReader.GetSheetProperty(OMRSheets, MyXML.OMREnums.OMRSheet.A550, MyXML.OMREnums.OMRProperty.tensBlock2));
                //gg.DrawRectangle(pr, MyXML.OMRSheetReader.GetSheetProperty(OMRSheets, MyXML.OMREnums.OMRSheet.A550, MyXML.OMREnums.OMRProperty.tensBlock3));
                //gg.DrawRectangle(pr, MyXML.OMRSheetReader.GetSheetProperty(OMRSheets, MyXML.OMREnums.OMRSheet.A550, MyXML.OMREnums.OMRProperty.tensBlock4));

                pr.Dispose();
                gg.Dispose();
                return (Bitmap)imgl;
            }
        }
        private System.Drawing.Point[] ToPointsArray(List<IntPoint> points)
        {
            System.Drawing.Point[] array = new System.Drawing.Point[points.Count];

            for (int i = 0, n = points.Count; i < n; i++)
            {
                array[i] = new System.Drawing.Point(points[i].X, points[i].Y);
            }

            return array;
        }
        private List<System.Drawing.Point> afPointListToSystem(List<IntPoint> points)
        {
            List<System.Drawing.Point> list_ = new List<System.Drawing.Point>();

            for (int i = 0, n = points.Count; i < n; i++)
            {
                list_.Add(new System.Drawing.Point(points[i].X, points[i].Y));
            }

            return list_;
        }

        private int rateSlice(Bitmap slice, int OMCount)
        {
            Rectangle[] cropRects = new Rectangle[OMCount];
            Bitmap[] marks = new Bitmap[OMCount];
            for (int i = 0; i < OMCount; i++)
            {
                cropRects[i] = new Rectangle(i * slice.Width / OMCount, 0, slice.Width / OMCount, slice.Height);
            }

            int crsr = 0;
            foreach (Rectangle cropRect in cropRects)
            {
                Bitmap target = new Bitmap(cropRect.Width, cropRect.Height);

                using (Graphics g = Graphics.FromImage(target))
                {
                    g.DrawImage(slice, new Rectangle(0, 0, target.Width, target.Height),
                                     cropRect,
                                     GraphicsUnit.Pixel);
                }
                marks[crsr] = target;
                crsr++;
            }
            long maxPD = (slice.Width / OMCount) * slice.Height * 255;
            List<long> inks = new List<long>();
            List<long> fullInks = new List<long>();

            foreach (Bitmap mark in marks)
            {
                inks.Add(InkDarkness(mark));
                fullInks.Add(inks[inks.Count - 1]);
            }
            int indofMx = -1, indofMn = -1;
            long maxD = 0, minD = 0; ;
            for (int i = 0; i < OMCount; i++)
            {
                if (inks[i] > maxD)
                {
                    maxD = inks[i];
                    indofMx = i;
                }
            }
            minD = maxD;
            for (int i = 0; i < OMCount; i++)
            {
                if (inks[i] < minD)
                {
                    minD = inks[i];
                    indofMn = i;
                }
            }
            for (int i = 0; i < OMCount; i++)
            {
                inks[i] -= minD - 1;
            }
            bool parallelExist = false, spe = false, tpe = false, fpe = false;
            for (int i = 0; i < OMCount; i++)
            {
                if (i != indofMx)
                {
                    if ((double)fullInks[indofMx] / fullInks[i] <= 2)
                    {
                        if (tpe) fpe = true;
                        if (spe) tpe = true;
                        if (parallelExist) spe = true;
                        parallelExist = true;
                    }
                }
            }
            int negScore = parallelExist ? -1 : 0;
            negScore = spe ? -2 : negScore;
            negScore = tpe ? -3 : negScore;
            negScore = fpe ? -4 : negScore;

            if (!parallelExist)
                return indofMx + 1;
            bool atleastOneUnfilled = false;
            for (int i = 0; i < OMCount; i++)
            {
                if (i != indofMx)
                {
                    if ((double)fullInks[indofMx] / fullInks[i] >= 3)
                        atleastOneUnfilled = true;
                }
            }
            if (atleastOneUnfilled)
                return negScore;


            return 0;
        }
        private long InkDarkness(Bitmap OMark)
        {
            int darkestC = 255, lightestC = 0;

            UnmanagedImage mark = UnmanagedImage.FromManagedImage(OMark);
            for (int y = 0; y < OMark.Height; y++)
                for (int x = 0; x < OMark.Width; x++)
                {
                    Color c = mark.GetPixel(x, y);
                    if (((c.R + c.G + c.B) / 3) > lightestC)
                    {
                        lightestC = ((c.R + c.G + c.B) / 3);
                    }
                    if (((c.R + c.G + c.B) / 3) < darkestC)
                    {
                        darkestC = ((c.R + c.G + c.B) / 3);
                    }
                }
            int dc = 0;
            for (int y = 0; y < OMark.Height; y++)
                for (int x = 0; x < OMark.Width; x++)
                {
                    Color c = mark.GetPixel(x, y);

                    if (((c.R + c.G + c.B) / 3) < (lightestC + darkestC) / 2)
                    { dc += 255; }
                }
            return dc;
        }
        private Bitmap[] SliceOMarkBlock(System.Drawing.Image fullSheet, Rectangle slicer, int slices)
        {
            List<Rectangle> cropRects = new List<Rectangle>();
            Bitmap[] bmps = new Bitmap[slices];
            for (int i = 0; i < slices; i++)
            {
                cropRects.Add(new Rectangle(slicer.X, slicer.Y + (slicer.Height / slices) * i, slicer.Width, slicer.Height / slices));
            }
            Bitmap src = (Bitmap)fullSheet;
            int crsr = 0;
            foreach (Rectangle cropRect in cropRects)
            {
                Bitmap target = new Bitmap(cropRect.Width, cropRect.Height);

                using (Graphics g = Graphics.FromImage(target))
                {
                    g.DrawImage(src, new Rectangle(0, 0, target.Width, target.Height),
                                     cropRect,
                                     GraphicsUnit.Pixel);
                }
                bmps[crsr] = target;
                crsr++;
            }
            return bmps;
            throw new Exception("Couldn't slice");
        }

    }
}