using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MMORPG_SERVER
{
    class Database
    {


      public bool AccountExists(int index, string username)
        {
            var db = Globals.mysql.DB_RS;
            {
                db.Open("SELECT * FROM accounts WHERE Username='"+username+"'", Globals.mysql.DB_CONN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic);
                
                if (db.EOF)
                {
                    Globals.networkSendData.SendAlertMsg(index, "Username does not exists!");
                    db.Close();

                    return false;
                }
                else
                {

                    
                    db.Close();

                    return true;
                }
            }
        }
      public bool PasswordOK(int index,string username,string password)
        {
            var db = Globals.mysql.DB_RS;
            {
                db.Open("SELECT'"+username+"' FROM accounts WHERE Password='" + password + "'", Globals.mysql.DB_CONN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic);

                if (db.EOF)
                {
                    Globals.networkSendData.SendAlertMsg(index, "Password does not match!");

                    db.Close();

                    return false;
                }
                else
                {

                    
                    db.Close();

                    return true;
                }
            }
        }

      public void AddAccount(string username, string password)
        {
            var db= Globals.mysql.DB_RS;
            {
                db.Open("SELECT * FROM accounts WHERE 0=1", Globals.mysql.DB_CONN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic);
                db.AddNew();
                db.Fields["Username"].Value = username;
                db.Fields["Password"].Value = password;
                db.Update();
                db.Close();
            }
        }

      public void LoadPlayer(int index, string username)
        {
            var db = Globals.mysql.DB_RS;
            {
                db.Open("SELECT * FROM accounts WHERE Username='"+username+"'", Globals.mysql.DB_CONN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic);
                Globals.player[index].Username = db.Fields["Username"].Value;
                Globals.player[index].posX = db.Fields["X"].Value;
                Globals.player[index].posX = db.Fields["Y"].Value;
                Globals.player[index].posX = db.Fields["Z"].Value;
                db.Close();
            }
        }

      public void SavePlayer(int index)
        {
            //var db = Globals.mysql.DB_RS;
            //{
            //    db.Open("SELECT * FROM accounts WHERE Username='" + Globals.player[index].Username + "'", Globals.mysql.DB_CONN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic);
            //    db.Fields["X"].Value = Globals.player[index].posX;
            //    db.Fields["Y"].Value = Globals.player[index].posY;
            //    db.Fields["Z"].Value = Globals.player[index].posZ;
            //    db.Update();
            //    db.Close();
            //}
        }
    }
}
