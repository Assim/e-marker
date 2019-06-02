package model;

import java.io.Serializable;
import java.util.ArrayList;

@SuppressWarnings("serial")
public class AnswerSheet extends Sheet implements Serializable {

	private String studentName;
	
	public AnswerSheet(String studentName, ArrayList<Integer> choices) {
		this.studentName = studentName;
		this.setChoices(choices);
	}
	
	public void setStudentName(String studentName) {
		this.studentName = studentName;
	}
	
	public String getStudentName() {
		return this.studentName;
	}

}
