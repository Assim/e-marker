package util;

import java.io.File;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.Statement;
import java.sql.Connection;

public class Db {

    private static final String CONNECTION_STRING = "jdbc:sqlite:emarker.db";
    
    private Db() {
 
    }
 
    public static void createTables() {
        Connection c = null;
        Statement stmt = null;
        try {
            Class.forName("org.sqlite.JDBC");
            c = DriverManager.getConnection(CONNECTION_STRING);
 
            stmt = c.createStatement();
            String sql = "CREATE TABLE IF NOT EXISTS TEACHER "
                    + "(ID INTEGER PRIMARY KEY     AUTOINCREMENT,"
                    + "USERNAME            TEXT    NOT NULL,"
                    + "PASSWORD TEXT    NOT NULL)";
            stmt.executeUpdate(sql);
 
            stmt.close();
            c.close();
        } catch (Exception e) {
            System.err.println(e.getClass().getName() + ": " + e.getMessage());
            System.exit(0);
        }
    }
    
    public static void addTeacher(String username, String password) {
    	Connection c = null;
    	Statement stmt = null;
    	try {
            Class.forName("org.sqlite.JDBC");
            c = DriverManager.getConnection(CONNECTION_STRING);
 
            stmt = c.createStatement();
            String sql = "INSERT INTO TEACHER (USERNAME, PASSWORD) "
                    + "VALUES ('"
                    + username
                    + "', '"
                    + password+ "' );";
            stmt.executeUpdate(sql);
 
            stmt.close();
            c.close();
    	} catch (Exception e) {
            System.err.println(e.getClass().getName() + ": " + e.getMessage());
            System.exit(0);
        }
    }
    
    public static boolean isTeacherNameExists(String username) {
        Connection c = null;
        Statement stmt = null;
        boolean result = false;
        try {
            Class.forName("org.sqlite.JDBC");
            c = DriverManager.getConnection(CONNECTION_STRING);
            c.setAutoCommit(false);
 
            stmt = c.createStatement();
            ResultSet rs = stmt
                    .executeQuery("SELECT * FROM TEACHER WHERE USERNAME = '"+username+"'");
            if(rs.next()) {
            	result = true;
            }
            rs.close();
            stmt.close();
            c.close();
        } catch (Exception e) {
            System.err.println(e.getClass().getName() + ": " + e.getMessage());
            System.exit(0);
        }
        return result;	
 
    }
    
    public static boolean userExists(String username, String password) {
        Connection c = null;
        Statement stmt = null;
        boolean result = false;
        try {
            Class.forName("org.sqlite.JDBC");
            c = DriverManager.getConnection(CONNECTION_STRING);
            c.setAutoCommit(false);
 
            stmt = c.createStatement();
            ResultSet rs = stmt
                    .executeQuery("SELECT * FROM TEACHER WHERE USERNAME = '"+username+"' AND PASSWORD = '"+password+"'");
            if(rs.next()) {
            	result = true;
            }
            rs.close();
            stmt.close();
            c.close();
        } catch (Exception e) {
            System.err.println(e.getClass().getName() + ": " + e.getMessage());
            System.exit(0);
        }
        return result;	
    }
 
    public static void deleteDb() {
        File file = new File("emarker.db");
        file.delete();
    }
}
