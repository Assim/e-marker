package model;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.io.Serializable;
import java.util.ArrayList;

@SuppressWarnings("serial")
public class Teacher implements Serializable {

	public static String FILE_NAME;
	
	private String username;
	private ArrayList<Exam> exams;
	
	public Teacher(String username) {
		exams = new ArrayList<Exam>();
		FILE_NAME = username+".ser";
		loadFromFile();
	}
	
	public void setUsername(String username) {
		this.username = username;
	}
	
	public String getUsername() {
		return this.username;
	}
	
	public void setExams(ArrayList<Exam> exams) {
		this.exams = exams;
	}
	
	public ArrayList<Exam> getExams() {
		return this.exams;
	}
	
	public void saveToFile() {
		try {
			FileOutputStream fileOut = new FileOutputStream(FILE_NAME);
			ObjectOutputStream out = new ObjectOutputStream(fileOut);
			out.writeObject(this);
			out.close();
			fileOut.close();
		} catch (IOException e) {
			e.printStackTrace();
		}
	}

	public void loadFromFile() {
		File file = new File(FILE_NAME);
		if (!file.exists())
			return; // Exit if file doesn't exist
		Teacher teacher = null;
		try {
			FileInputStream fileIn = new FileInputStream(FILE_NAME);
			ObjectInputStream in = new ObjectInputStream(fileIn);
			teacher = (Teacher) in.readObject(); // convert object to exam
			in.close();
			fileIn.close();
		} catch (IOException | ClassNotFoundException e) {
			e.printStackTrace();
		}

		// Set values from file to this object
		this.setUsername(teacher.getUsername());
		this.setExams(teacher.getExams());
		
	}

	public void deleteFile() {
		File file = new File(FILE_NAME);
		file.delete();
	}
	
	public boolean fileExists() {
		File file = new File(FILE_NAME);
		if(file.exists()) return true;
		else return false;
	}
	
}
