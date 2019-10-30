using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using MySql.Data;
using MySql.Data.MySqlClient;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    string SERVER = "127.0.0.1";
    string DATABASE = "data";
    string USERID = "root";
    string PORT = "3306";
    string PASSWORD = "root";
    MySqlConnection conn;
    InputField inputField1;
    InputField inputField2;
    InputField inputField3;

    void Start()
    {

        string connCmd =
               "server=" + SERVER + ";" +
               "database=" + DATABASE + ";" +
               "userid=" + USERID + ";" +
               "port=" + PORT + ";" +
               "password=" + PASSWORD;

        conn = new MySqlConnection(connCmd);

        try
        {
            Debug.Log("MySQLと接続中...");
            conn.Open();

        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void SignUpOnClick()
    {
        //値の取得
        inputField1 = GameObject.Find("ID-BOX").GetComponent<InputField>();
        string name = inputField1.text;
        inputField2 = GameObject.Find("PASS-BOX").GetComponent<InputField>();
        string password1 = inputField2.text;
        inputField3 = GameObject.Find("Pass-Confirmation").GetComponent<InputField>();
        string password2 = inputField3.text;
        string cryptPass = AvoEx.AesEncryptor.Encrypt(password1);

        //        string decText = AvoEx.AesEncryptor.Decrypt(encText); →復号化


        Debug.Log("encText = " + cryptPass);
        if (String.IsNullOrWhiteSpace(cryptPass) == true || String.IsNullOrWhiteSpace(name) == true
           || password1 != password2)
        {
            SceneManager.LoadScene("Failed");
        }
        else
        {
            String sql = "INSERT INTO LOGIN VALUES(\'" + name + "\',\'" + cryptPass + "\');";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            try
            { cmd.ExecuteNonQuery();
                SceneManager.LoadScene("Success");
            } catch (Exception e)
            {
                SceneManager.LoadScene("Failed");
            }


            //            MySqlDataReader rdr = cmd.ExecuteReader();
            //            while (rdr.Read())
            //            {
            //                Debug.Log(rdr[0]);
            //            }
            //            rdr.Close();
            //

            conn.Close();
            Debug.Log("接続を終了しました");
        }

    }
    public void LoginOnClick()
    {
        
        inputField1 = GameObject.Find("ID-BOX").GetComponent<InputField>();
        string name = inputField1.text;
        inputField2 = GameObject.Find("PASS-BOX").GetComponent<InputField>();
        string password = inputField2.text;
        string cryptPass = AvoEx.AesEncryptor.Encrypt(password);
        //        string decText = AvoEx.AesEncryptor.Decrypt(password);
        String sql = "SELECT * FROM LOGIN WHERE NAME = \'+name+\' AND PASSWORD = \'+cryptPass+\';";
        MySqlCommand cmd = new MySqlCommand(sql, conn);

        try
        {
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Debug.Log(rdr[0]);
            }
            rdr.Close();
            SceneManager.LoadScene("Success");
        }
        catch(Exception e){
            SceneManager.LoadScene("Failed");
        }

            conn.Close();
            Debug.Log("接続を終了しました");
        }

}
