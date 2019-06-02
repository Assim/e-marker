package view;

import java.awt.EventQueue;

import javax.swing.JFrame;
import javax.swing.JOptionPane;
import javax.swing.JPanel;
import javax.swing.border.EmptyBorder;

import java.awt.Color;

import javax.swing.JLabel;

import java.awt.Font;
import java.util.ArrayList;

import javax.swing.JTextField;
import javax.swing.JScrollPane;

import javax.swing.JButton;

import model.Exam;
import model.Teacher;

import java.awt.event.ActionListener;
import java.awt.event.ActionEvent;
import javax.swing.ImageIcon;
import javax.swing.BoxLayout;

@SuppressWarnings("serial")
public class AddExamView extends JFrame {
	
	private JPanel contentPane;
	private JTextField textField;
	private JTextField textField_1;
	private JPanel panel;
	private ArrayList<JTextField> questionFields = new ArrayList<JTextField>();
	private JScrollPane scrollPane;

	/**
	 * Launch the application.
	 */
	public static void main(String[] args) {
		EventQueue.invokeLater(new Runnable() {
			public void run() {
				try {
					AddExamView frame = new AddExamView(null);
					frame.setVisible(true);
				} catch (Exception e) {
					e.printStackTrace();
				}
			}
		});
	}

	/**
	 * Create the frame.
	 */
	public AddExamView(final Teacher teacher) {
		setTitle("Add Exam");
		setBounds(100, 100, 450, 458);
		contentPane = new JPanel();
		contentPane.setBackground(Color.WHITE);
		contentPane.setBorder(new EmptyBorder(5, 5, 5, 5));
		setContentPane(contentPane);
		contentPane.setLayout(null);
		
		JLabel lblNewLabel = new JLabel("Add New Exam..");
		lblNewLabel.setFont(new Font("Times New Roman", Font.BOLD, 14));
		lblNewLabel.setBounds(20, 73, 122, 14);
		contentPane.add(lblNewLabel);
		
		JLabel lblNewLabel_1 = new JLabel("Exam Name :");
		lblNewLabel_1.setBounds(20, 98, 67, 14);
		contentPane.add(lblNewLabel_1);
		
		JLabel lblNewLabel_2 = new JLabel("Exam ID :");
		lblNewLabel_2.setBounds(20, 120, 67, 14);
		contentPane.add(lblNewLabel_2);
		
		textField = new JTextField();
		textField.setBounds(94, 95, 228, 20);
		contentPane.add(textField);
		textField.setColumns(10);
		
		textField_1 = new JTextField();
		textField_1.setBounds(94, 117, 228, 20);
		contentPane.add(textField_1);
		textField_1.setColumns(10);
	
		panel = new JPanel();
		panel.setBackground(Color.WHITE);
		panel.setBounds(10, 89, 414, 267);
	
		scrollPane = new JScrollPane(panel);
		panel.setLayout(new BoxLayout(panel, BoxLayout.Y_AXIS));
		scrollPane.setBounds(10, 148, 414, 227);
		scrollPane.setHorizontalScrollBarPolicy(JScrollPane.HORIZONTAL_SCROLLBAR_ALWAYS);
		scrollPane.setVerticalScrollBarPolicy(JScrollPane.VERTICAL_SCROLLBAR_ALWAYS);
		contentPane.add(scrollPane);
		
		JButton btnAddExam = new JButton("Add Exam");
		btnAddExam.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				if(questionFields.get(0).getText().length() == 0) {
					JOptionPane.showMessageDialog(null, "Please make sure that the first option is filled.");
					return;
				}
				
				int examId = 0;
				try {
					examId = Integer.parseInt(textField_1.getText());	
				} catch(Exception ex) {
					JOptionPane.showMessageDialog(null, "Please enter a number as exam ID");
					return;
				}
				
				String examName = textField.getText();
				
				if(examName.length() == 0) {
					JOptionPane.showMessageDialog(null, "Exam name can't be empty.");
					return;
				}
				
				Exam exam = new Exam(examId, examName);
				
				ArrayList<Integer> choices = new ArrayList<Integer>();
				for(int i=0; i<40; i++) {
					String x = questionFields.get(i).getText();
					if(x.length() == 0) {
						break;
					}
					
					try {
						int currentChoice = Integer.parseInt(x);
						
						if(currentChoice < 1 || currentChoice > 5) {
							JOptionPane.showMessageDialog(null, "Make sure options are numbers from 1-5");
							return;
						}
					} catch (Exception ex) {
						JOptionPane.showMessageDialog(null, "Make sure options are numbers from 1-5");
						return;
					}
					
					choices.add(Integer.parseInt(x));
				}

				exam.getKeySheet().setChoices(choices);
				teacher.getExams().add(exam);
				teacher.saveToFile();
		
				new ExamView(teacher).setVisible(true);
				dispose();
			}
		});
		btnAddExam.setBounds(20, 386, 122, 23);
		contentPane.add(btnAddExam);
		
		JButton btnCancel = new JButton("Cancel");
		btnCancel.setBounds(150, 386, 89, 23);
		contentPane.add(btnCancel);
		
		JLabel lblNewLabel_3 = new JLabel("New label");
		lblNewLabel_3.setIcon(new ImageIcon("C:\\Users\\Personal\\Desktop\\hct\\course project 2014\\second_page.png"));
		lblNewLabel_3.setBounds(20, 0, 374, 74);
		contentPane.add(lblNewLabel_3);
		
		for(int i=0; i<40; i++) {
			JLabel tempLabel = new JLabel("<html>Qn"+(i+1)+"<br></html>");
			JTextField tempField = new JTextField();
			tempField.setColumns(1);
			panel.add(tempLabel);
			panel.add(tempField);
			questionFields.add(tempField);
		}
	}
}
