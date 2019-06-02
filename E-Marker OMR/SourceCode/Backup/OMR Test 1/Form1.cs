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

namespace OMRReader_test1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ts = new TimeSpan(DateTime.Now.Ticks);
            textBox1.Text = "";
            showTimeStamp("Process Started");
            if (!loadLast_cb.Checked)
                panel1.BackgroundImage = System.Drawing.Image.FromFile(filenamet.Text);
            showTimeStamp("Image Read");
            panel1.BackgroundImage = (System.Drawing.Image)ImageUtilities.ResizeImage((Bitmap)panel1.BackgroundImage, 2100, 2100 * panel1.BackgroundImage.Height / panel1.BackgroundImage.Width);
            panel1.Invalidate();
            Application.DoEvents();
            showTimeStamp("Resized");
            Application.DoEvents();
            Bitmap unf = new Bitmap(panel1.BackgroundImage);
            //OMRReader reader = new OMRReader();
            //panel1.BackgroundImage = (System.Drawing.Image)reader.ExtractOMRSheet(unf, "sheets.xml", ref panel1, ref textBox1, MyXML.OMREnums.OMRSheet.A550);
            panel1.BackgroundImage = (System.Drawing.Image)ExtractOMRSheet(unf, fill_tb.Value, con_tb.Value, "A540.omr");
            showTimeStamp("OMR Extraction Finished");
            panel1.Invalidate();

        }
        /// <summary>
        /// Extracts OMR Sheet from any image
        /// </summary>
        /// <param name="basicImage">Image to start Process with</param>
        /// <param name="fillint">Preffered to be 0 for low lightning conditions</param>
        /// <param name="contint">Preffered to be 0 for low lightning conditions</param>
        /// <returns></returns>
        private Bitmap ExtractOMRSheet(Bitmap basicImage, int fillint, int contint, string OMRSpecsSheetAddress)
        {
            showTimeStamp("Flatting Started");
            panel1.BackgroundImage = (System.Drawing.Image)flatten(basicImage, fillint, contint);
            showTimeStamp("Flattened");
            panel1.Invalidate();
            Application.DoEvents();
            showTimeStamp("OMR Extraction Started");
            return ExtractPaperFromFlattened(new Bitmap(panel1.BackgroundImage), basicImage, minblb_tb.Value, fillint, contint, OMRSpecsSheetAddress);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            filenamet.Text = (string)OMR.RegistryEditor.ReadReg(this.Name, filenamet.Name);
            fill_tb.Value = (int)OMR.RegistryEditor.ReadReg(this.Name, fill_tb.Name);
            minblb_tb.Value = (int)OMR.RegistryEditor.ReadReg(this.Name, minblb_tb.Name);
            con_tb.Value = (int)OMR.RegistryEditor.ReadReg(this.Name, con_tb.Name);
            thresh_tb.Value = (int)OMR.RegistryEditor.ReadReg(this.Name, thresh_tb.Name);
            sim_tb.Value = (int)OMR.RegistryEditor.ReadReg(this.Name, sim_tb.Name);
            if (Convert.ToBoolean(OMR.RegistryEditor.ReadReg(this.Name, loadLast_cb.Name)))
            {
                panel1.BackgroundImage = System.Drawing.Image.FromFile("Lastimg.jpg");
                loadLast_cb.Checked = Convert.ToBoolean(OMR.RegistryEditor.ReadReg(this.Name, loadLast_cb.Name));
            }
        }
        public Bitmap flatten(Bitmap bmp,int fillint,int contint)
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
            AForge.Imaging.Filters.Threshold thresh_hold = new Threshold(thresh_tb.Value);
            bmp = invert.Apply(thresh_hold.Apply(extract_channel.Apply(Contrast.Apply(bmp))));

            return bmp;
        }
        private bool isSame(UnmanagedImage img1, UnmanagedImage img2)
        {
            int count = 0, tcount = img2.Width * img2.Height;
            for (int y = 0; y< img1.Height;y++)
                for (int x = 0; x < img1.Width; x++)
                {
                    Color c1 = img1.GetPixel(x,y), c2 = img2.GetPixel(x,y);
                    if ((c1.R + c1.G + c1.B) / 3 > (c2.R + c2.G + c2.B) / 3 - 10 && 
                        (c1.R + c1.G + c1.B) / 3 < (c2.R + c2.G + c2.B) / 3 + 10)
                        count++;
                }
            return (count * 100) / tcount >= sim_tb.Value;
        }
        TimeSpan ts = new TimeSpan(DateTime.Now.Ticks);
        private void showTimeStamp(string str)
        {
            textBox1.AppendText(str + ": " + (new TimeSpan(DateTime.Now.Ticks) - ts).TotalSeconds + "\r\n");
            ts = new TimeSpan(DateTime.Now.Ticks);
        }
        private Bitmap ExtractPaperFromFlattened(Bitmap bitmap,Bitmap basicImage,int minBlobWidHei,int fillint,int contint,string OMRSheets)
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
            showTimeStamp("Left edge Dection Started");
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
            showTimeStamp("Left edge Detection Ends");
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
            showTimeStamp("Right edge Dection Started");
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
            showTimeStamp("Right edge Dection Ends");
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
                        return ExtractOMRSheet(basicImage, fillint, contint,OMRSheets);
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
                return(Bitmap) imgl;
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

        private void fill_tb_Scroll(object sender, EventArgs e)
        {
            OMR.RegistryEditor.WriteRegStr(this.Name, fill_tb.Name, fill_tb.Value);
            label4.Text = fill_tb.Value.ToString();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            OMR.RegistryEditor.WriteRegStr(this.Name, minblb_tb.Name, minblb_tb.Value);
            label5.Text = minblb_tb.Value.ToString();
        }

        private void filenamet_TextChanged(object sender, EventArgs e)
        {
            OMR.RegistryEditor.WriteRegStr(this.Name, filenamet.Name, filenamet.Text);
        }

        private void trackBar1_Scroll_1(object sender, EventArgs e)
        {
            OMR.RegistryEditor.WriteRegStr(this.Name, con_tb.Name, con_tb.Value);
            label6.Text = con_tb.Value.ToString();
        }

        private void thresh_tb_Scroll(object sender, EventArgs e)
        {
            OMR.RegistryEditor.WriteRegStr(this.Name, thresh_tb.Name, thresh_tb.Value);
            label8.Text = thresh_tb.Value.ToString();
        }

        private void trackBar1_Scroll_2(object sender, EventArgs e)
        {
            OMR.RegistryEditor.WriteRegStr(this.Name, sim_tb.Name, sim_tb.Value);
            label10.Text = sim_tb.Value.ToString();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            OMR.RegistryEditor.WriteRegStr(this.Name, loadLast_cb.Name, loadLast_cb.Checked);
            loadLast_cb.Checked = loadLast_cb.Checked ? !(loadLast_cb.Checked && panel1.BackgroundImage == null) : false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OMRSheetSpecsWriter.WriteSheetsToFile("sheets.xml");
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ts = new TimeSpan(DateTime.Now.Ticks);
            textBox1.Text = "";
            showTimeStamp("Process Started");
            if (!loadLast_cb.Checked)
                panel1.BackgroundImage = System.Drawing.Image.FromFile(filenamet.Text);
            showTimeStamp("Image Read");
            panel1.BackgroundImage = (System.Drawing.Image)ImageUtilities.ResizeImage((Bitmap)panel1.BackgroundImage, 2100, 2100 * panel1.BackgroundImage.Height / panel1.BackgroundImage.Width);
            panel1.Invalidate();
            Application.DoEvents();
            showTimeStamp("Resized");
            Application.DoEvents();
            Bitmap unf = new Bitmap(panel1.BackgroundImage);
            OpticalReader reader = new OpticalReader();
            panel1.BackgroundImage = (System.Drawing.Image)reader.ExtractOMRSheet(unf, "sheets.xml", ref panel1, ref textBox1, OMREnums.OMRSheet.A555);
            //panel1.BackgroundImage = (System.Drawing.Image)ExtractOMRSheet(unf, fill_tb.Value, con_tb.Value, "A540.omr");
            showTimeStamp("OMR Extraction Finished");
            panel1.Invalidate();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ts = new TimeSpan(DateTime.Now.Ticks);
            textBox1.Text = "";
            showTimeStamp("Process Started");
            if (!loadLast_cb.Checked)
                panel1.BackgroundImage = System.Drawing.Image.FromFile(filenamet.Text);
            showTimeStamp("Image Read");
            panel1.BackgroundImage = (System.Drawing.Image)ImageUtilities.ResizeImage((Bitmap)panel1.BackgroundImage, 2100, 2100 * panel1.BackgroundImage.Height / panel1.BackgroundImage.Width);
            panel1.Invalidate();
            Application.DoEvents();
            showTimeStamp("Resized");
            Application.DoEvents();
            Bitmap unf = new Bitmap(panel1.BackgroundImage);
            OpticalReader reader = new OpticalReader();
            panel1.BackgroundImage = (System.Drawing.Image)reader.ExtractOMRSheet(unf, "sheets.xml" , OMREnums.OMRSheet.A550);
            showTimeStamp("OMR Extraction Finished");
            panel1.Invalidate();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //ContrastCorrection cc = new ContrastCorrection();
            //panel1.BackgroundImage = cc.Apply((Bitmap)panel1.BackgroundImage);
            panel1.Invalidate(); Application.DoEvents();
            showTimeStamp("Slicing Started");
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
                bmps.Add(SliceOMarkBlock(panel1.BackgroundImage, Blocks[i], 10));
            }
            showTimeStamp("Slicing ended");

            string ans = "";
            foreach (Bitmap[] blk in bmps)
            {
                foreach (Bitmap line in blk)
                {
                    ans += rateSlice(line,5) + ",";
                }
                ans += "\r\n";
            }

            panel1.Invalidate();
            Application.DoEvents();
            MessageBox.Show(ans);

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
            int indofMx = -1,indofMn = -1;
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
                cropRects.Add(new Rectangle(slicer.X, slicer.Y + (slicer.Height / slices) * i, slicer.Width, slicer.Height/slices));
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

        private void button8_Click(object sender, EventArgs e)
        {
            OpticalReader rr = new OpticalReader();
            MessageBox.Show("Found registration number: " + rr.getRegNumOfSheet(panel1.BackgroundImage, OMREnums.OMRSheet.A550, "sheets.xml", false).ToString());
        }

        private void button9_Click(object sender, EventArgs e)
        {
            AnswerKeyMaker rrf = new AnswerKeyMaker("Sheets.xml");
            rrf.StartPosition = FormStartPosition.CenterParent;
            rrf.ShowDialog(this);
        }
    }
}
