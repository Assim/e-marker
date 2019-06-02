package view;

import java.awt.EventQueue;

import javax.swing.JFrame;
import javax.swing.JOptionPane;
import javax.swing.JPanel;
import javax.swing.border.EmptyBorder;
import javax.swing.JLabel;
import javax.swing.ImageIcon;

import java.awt.Color;
import java.awt.Font;

import javax.swing.JTextField;
import javax.swing.JButton;

import util.Db;

import java.awt.event.ActionListener;
import java.awt.event.ActionEvent;
import java.util.ArrayList;

import javax.swing.JPasswordField;

import model.AnswerSheet;
import model.Exam;
import model.Teacher;

@SuppressWarnings("serial")
public class LoginView extends JFrame {

	private JPanel contentPane;
	private JTextField txtUser;
	private JPasswordField passwordField;

	/**
	 * Launch the application.
	 */
	public static void main(final String[] args) {
		EventQueue.invokeLater(new Runnable() {
			public void run() {
				try {
					if(args.length > 0) {
						String teacher = args[0];
						String student = args[1];
						int examId = Integer.parseInt(args[2]);
						
						String answers = args[3];
						answers = answers.substring(0, answers.length()-1); // remove last char because it's a , to prevent error
						String[] split = answers.split(",");
						ArrayList<Integer> choices = new ArrayList<Integer>();
						for(String x: split) {
							choices.add(Integer.parseInt(x));
						}
						
						Teacher t = new Teacher(teacher);
						
						Exam foundExam = null;
						ArrayList<Exam> exams = t.getExams();
						for(Exam exam:exams) {
							if(exam.getExamId() == examId) {
								foundExam = exam;
								break;
							}
						}
						
						if(foundExam == null) {
							JOptionPane.showMessageDialog(null, "Exam ID was not found");
						}
						else {
							AnswerSheet answerSheet = new AnswerSheet(student, choices);
							foundExam.getAnswerSheets().add(answerSheet);
							t.saveToFile();
							JOptionPane.showMessageDialog(null, "Student sheet added");
							// Show Exam View
							new ExamView(t).setVisible(true);
						}
					}
					else {
						LoginView frame = new LoginView();
						frame.setVisible(true);						
					}
				} catch (Exception e) {
					e.printStackTrace();
				}
			}
		});
	}

	/**
	 * Create the frame.
	 */
	public LoginView() {
		
		Db.createTables();
		setTitle("E-Marker");
		setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		setBounds(100, 100, 604, 364);
		contentPane = new JPanel();
		contentPane.setBackground(Color.WHITE);
		contentPane.setBorder(new EmptyBorder(5, 5, 5, 5));
		setContentPane(contentPane);
		contentPane.setLayout(null);
		
		JLabel label = new JLabel("");
		label.setIcon(new ImageIcon("C:\\Users\\Personal\\Desktop\\hct\\course project 2014\\FIRST_LOGO.png"));
		label.setBounds(10, 11, 568, 187);
		contentPane.add(label);
		
		JPanel panel = new JPanel();
		panel.setBackground(Color.WHITE);
		panel.setBounds(20, 193, 558, 124);
		contentPane.add(panel);
		panel.setLayout(null);
		
		JLabel lblUsername = new JLabel("Username  :");
		lblUsername.setFont(new Font("Times New Roman", Font.PLAIN, 16));
		lblUsername.setBounds(104, 27, 101, 17);
		panel.add(lblUsername);
		
		JLabel lblPassword = new JLabel("Password  :");
		lblPassword.setFont(new Font("Times New Roman", Font.PLAIN, 16));
		lblPassword.setBounds(105, 55, 73, 17);
		panel.add(lblPassword);
		
		txtUser = new JTextField();
		txtUser.setBounds(191, 24, 256, 20);
		panel.add(txtUser);
		txtUser.setColumns(10);
		
		
		JButton btnLogin = new JButton("Login");
		btnLogin.setFont(new Font("Times New Roman", Font.PLAIN, 14));
		
		btnLogin.addActionListener(new ActionListener() {
			@SuppressWarnings("deprecation")
			public void actionPerformed(ActionEvent arg0) {
				int count=0;
				if(Db.userExists(txtUser.getText(), passwordField.getText())) {
					// user exists
					Teacher t = new Teacher(txtUser.getText());
					new ExamView(t).setVisible(true);
					dispose();
				}
				else {
					 //user doesn't exist
					JOptionPane.showMessageDialog(null, "User not found");
					count++;
					if(count==3){
						System.exit(0);
					}
				}
			}
		});
		btnLogin.setBounds(205, 86, 101, 23);
		panel.add(btnLogin);
		
		JButton btnSignUp = new JButton("Sign Up");
		btnSignUp.setFont(new Font("Times New Roman", Font.PLAIN, 14));
		btnSignUp.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent arg0) {
				String username = JOptionPane.showInputDialog("Enter your username:");
				if(!Db.isTeacherNameExists(username)) {
					int password = (int)(Math.random()*99999);
					JOptionPane.showMessageDialog(null, "Your password is: "+String.valueOf(password));
					Db.addTeacher(username, String.valueOf(password));
				}
				else {
					JOptionPane.showMessageDialog(null, "Username already used.");
				}
			}
		});
		btnSignUp.setBounds(316, 86, 89, 23);
		panel.add(btnSignUp);
		
		passwordField = new JPasswordField();
		passwordField.setBounds(191, 55, 256, 20);
		panel.add(passwordField);
	}
}
