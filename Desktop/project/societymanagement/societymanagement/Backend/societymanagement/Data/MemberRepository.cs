using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;
using societymanagement.Entity;

namespace societymanagement.Data
{
    public class MemberRepository
    {
        private readonly SqlConnection _connection;


        public MemberRepository()
        {
            try
            {
                string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=societymanagement_new;Integrated Security=True;TrustServerCertificate=True;";
                _connection = new SqlConnection(connectionString);
                _connection.Open();  // Test connection
                Console.WriteLine("Database connection successful!");
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database connection failed: {ex.Message}");
            }
        }


        //this method fetch all the data
         public List<Members> GetAllMembers()
        {
            List<Members> list = new List<Members>();

            try
            {
              SqlCommand cmd = new SqlCommand("sp_GetMembers1", _connection);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    Members member = new Members
                    {
                        MemberId = Convert.ToInt32(row["MemberId"]),
                        Firstname = row["Firstname"].ToString(),
                        Lastname = row["Lastname"].ToString(),
                        Email = row["Email"].ToString(),
                        Password = row["Password"].ToString(), // Consider hashing before storing
                        PhoneNumber = row["PhoneNumber"].ToString(),
                      
                        FlatNumber = row["FlatNumber"].ToString(),
                        BlockNumber = row["BlockNumber"].ToString()
                    };

                    list.Add(member);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching members", ex);
            }

            return list;
        }

        //get specific email fetch
        public Members GetMemberByEmail(string email)
        {
            try
            {
                _connection.Open();

                using (SqlCommand cmd = new SqlCommand("sp_getuserbyemail", _connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@email", email);
                    //cmd.Parameters.AddWithValue("", email);


                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        DataRow row = dataTable.Rows[0];

                        return new Members
                        {
                            MemberId = Convert.ToInt32(row["MemberId"]),
                            Firstname = row["Firstname"].ToString(),
                            Lastname = row["Lastname"].ToString(),
                            //Email = row["Email"].ToString(),
                            //Password = row["Password"].ToString(),
                            PhoneNumber = row["PhoneNumber"].ToString(),
                     
                            FlatNumber = row["FlatNumber"].ToString(),
                            BlockNumber = row["BlockNumber"].ToString(),
                          //ImageUrl = row.IsNull("ImageURL") ? "" : row["ImageURL"].ToString()
                        };
          }       
                    else
                    {
                        return null; // Member not found
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching member by email", ex);
            }
            finally
            {
                _connection.Close();
            }
        }



        //    //this method is send the data in database

        public string AddMember(Members member)
        {
            try
            {
                _connection.Open(); // Open connection before executing

                using (SqlCommand checkemailcmd = new SqlCommand("sp_countmemnber1", _connection))
                {
                    checkemailcmd.CommandType = CommandType.StoredProcedure;
                    checkemailcmd.Parameters.AddWithValue("@email", member.Email);
                    int emailcount = (int)checkemailcmd.ExecuteScalar();

                    if (emailcount > 0)
                    {
                               return "Error: Email is already registered";
                     }
                }
                using (SqlCommand checkemailblock = new SqlCommand("sp_countflat", _connection))
                {
                    checkemailblock.CommandType = CommandType.StoredProcedure;
                    checkemailblock.Parameters.AddWithValue("@flatNumber", member.FlatNumber);
                    checkemailblock.Parameters.AddWithValue("@blocknumber", member.BlockNumber);
                    int flatblockcount = (int)checkemailblock.ExecuteScalar();

                    if (flatblockcount > 0)
                    {
                                return "Error: Flat Number Registered In This Block....";
                    }
                }


                //string hashedPassword = BCrypt.Net.BCrypt.HashPassword(member.Password);

                //string hashedPassword = BCrypt.Net.BCrypt.HashPassword(member.Password);


                using (SqlCommand cmd = new SqlCommand("sp_PostMember", _connection))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@firstname", member.Firstname);
                            cmd.Parameters.AddWithValue("@lastname", member.Lastname);
                            cmd.Parameters.AddWithValue("@email", member.Email);
                            cmd.Parameters.AddWithValue("@password", member.Password);
                            cmd.Parameters.AddWithValue("@phoneNumber", member.PhoneNumber);
                            
                            cmd.Parameters.AddWithValue("@flatNumber", member.FlatNumber);
                            cmd.Parameters.AddWithValue("@blockNumber", member.BlockNumber);

                            cmd.ExecuteNonQuery();

                            return "Member Added Successfully";
                        }


                    


               
            }
            catch (Exception ex)
            {
                throw new Exception("Error Adding New Member", ex);
            }
         
        }


    public String deletemember(int memberid)
    {
      try
      {
        _connection.Open();

        using (SqlCommand checkdeletemember = new SqlCommand("sp_deletemember", _connection))
        {
          checkdeletemember.CommandType = CommandType.StoredProcedure;
          checkdeletemember.Parameters.AddWithValue("@Memberid",memberid);

          int rowsAffected = checkdeletemember.ExecuteNonQuery();

          if (rowsAffected > 0)
          {
            return "Your profile has been deleted successfully.";
          }
          else
          {
            return "Error: Member ID not found.";
          }
        }

      }
      catch (Exception ex)
      {
        throw new Exception("Failed to delete your profile. Please try again later.", ex);
      }
      finally
      {
        _connection.Close();
      }
    }

    public async Task<string> updatememberAsync(EditMember members)
    {
      if (members.ImageFile != null)
      {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "imageupload");

        if (!Directory.Exists(path))
        {
          Directory.CreateDirectory(path);
        }

        var filePath = Path.Combine(path, $"{Guid.NewGuid()}_{Path.GetFileName(members.ImageFile.FileName)}");
        using (var fs = new FileStream(filePath, FileMode.Create))
        {
          await members.ImageFile.CopyToAsync(fs);
        }
        members.ImageUrl = Path.GetFileName(filePath);
      }

      try
      {


        _connection.Open();

        using (SqlCommand command = new SqlCommand("sp_UpdateProfile", _connection))
        {
          command.CommandType = CommandType.StoredProcedure;

          command.Parameters.AddWithValue("@MemberId", members.MemberId);
          command.Parameters.AddWithValue("@imageurl", members.ImageUrl ?? (object)DBNull.Value);
          command.Parameters.AddWithValue("@Firstname", members.Firstname);
          command.Parameters.AddWithValue("@Lastname", members.Lastname);
          //command.Parameters.AddWithValue("@Email", members.Email);
          //command.Parameters.AddWithValue("@Password", members.Password);
          command.Parameters.AddWithValue("@PhoneNumber", members.PhoneNumber);
          command.Parameters.AddWithValue("@FlatNumber", members.FlatNumber);
          command.Parameters.AddWithValue("@BlockNumber", members.BlockNumber);

          // Ensure connection is open before execution
          if (_connection.State != ConnectionState.Open)
          {
            return "Error: Database connection is closed before executing the query.";
          }

          int rowsAffected = await command.ExecuteNonQueryAsync();



          //return "Member updated successfully";


          if (rowsAffected > 0)
          {
            return "Member updated successfully";
          }
          else
          {
            return "No records were updated. MemberId may not exist.";
          }



        }
      }
      catch (Exception ex)
      {
        return $"Error: {ex.Message}";
      }
    }

    //public Members FetchAfterUpdate(int memberid,EditMember member)
    //{
    //  try
    //  {
    //    _connection.Open();

    //    using (SqlCommand cmd = new SqlCommand("sp_FetchALldataAfterUpdate", _connection))
    //    {
    //      cmd.CommandType = CommandType.StoredProcedure;
    //      cmd.Parameters.AddWithValue("@Member_Id", memberid);
    //      //cmd.Parameters.AddWithValue("", email);


    //      SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
    //      DataTable dataTable = new DataTable();
    //      dataAdapter.Fill(dataTable);

    //      if (dataTable.Rows.Count > 0)
    //      {
    //        DataRow row = dataTable.Rows[0];

    //        return new Members
    //        {
    //          MemberId = Convert.ToInt32(row["MemberId"]),
    //          Firstname = row["Firstname"].ToString(),
    //          Lastname = row["Lastname"].ToString(),
    //          //Email = row["Email"].ToString(),
    //          //Password = row["Password"].ToString(),
    //          PhoneNumber = row["PhoneNumber"].ToString(),

    //          FlatNumber = row["FlatNumber"].ToString(),
    //          BlockNumber = row["BlockNumber"].ToString(),
    //          ImageUrl = row.IsNull("ImageURL") ? "" : row["ImageURL"].ToString()
    //        };
    //      }
    //      else
    //      {
    //        return null; // Member not found
    //      }
    //    }
    //  }
    //  catch (Exception ex)
    //  {
    //    throw new Exception("Error fetching member by email", ex);
    //  }
    //  finally
    //  {
    //    _connection.Close();
    //  }
    //}


    public EditMember FetchAfterUpdate(int memberid)
    {
      try
      {
        if (_connection.State != ConnectionState.Open)
        {
          _connection.Open();
        }

        using (SqlCommand cmd = new SqlCommand("sp_FetchALldataAfterUpdate", _connection))
        {
          cmd.CommandType = CommandType.StoredProcedure;
          cmd.Parameters.AddWithValue("@Member_Id", memberid);

          using (SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd))
          {
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            if (dataTable.Rows.Count > 0)
            {
              DataRow row = dataTable.Rows[0];

              return new EditMember
              {
                MemberId = Convert.ToInt32(row["MemberId"]),
                Firstname = row["Firstname"].ToString(),
                Lastname = row["Lastname"].ToString(),
                PhoneNumber = row["PhoneNumber"].ToString(),
                FlatNumber = row["FlatNumber"].ToString(),
                BlockNumber = row["BlockNumber"].ToString(),
                ImageUrl = row.IsNull("ImageURL") ? "" : row["ImageURL"].ToString()
              };
            }
            else
            {
              return null; // No member found
            }
          }
        }
      }
      catch (SqlException sqlEx)
      {
        throw new Exception("SQL Error: " + sqlEx.Message, sqlEx);
      }
      catch (Exception ex)
      {
        throw new Exception("General Error: " + ex.Message, ex);
      }
      finally
      {
        if (_connection.State == ConnectionState.Open)
        {
          _connection.Close();
        }
      }
    }




  }
}
