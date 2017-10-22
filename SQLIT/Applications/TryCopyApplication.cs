using System;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace SQLIntegrationTool.Applications
{
    internal class TryCopyApplication : Application
    {
        public override Task Run(CommandLineArguments obj)
        {
            if (obj == null)
                throw new ArgumentException("Arguments are not correct");

            return Task.Run(() => {
                try
                {
                    SqlConnection connectionFrom = new SqlConnection("Data Source=sql6001.smarterasp.net;Initial Catalog=DB_A2B79F_smdbsql;User Id=DB_A2B79F_smdbsql_admin;Password=sjfhueii25gbf;");
                    SqlConnection connectionTo = new SqlConnection("Data Source=SERGEY_PC;Initial Catalog=local_smdbsql;User Id=SERGEY_PC\\SERGEY;Integrated Security=True");

                    connectionFrom.Open();
                    connectionTo.Open();


                    SqlDataAdapter adapterFrom = new SqlDataAdapter("SELECT * FROM INFORMATION_SCHEMA.TABLES", connectionFrom);
                    DataSet ds = new DataSet();
                    adapterFrom.Fill(ds);

                    //getting tables
                    foreach (DataRow tableRow in ds.Tables[0].Rows)
                    {
                        String currentTableName = (String)tableRow["TABLE_NAME"];

                        SqlDataAdapter currentTableAdapterFrom = new SqlDataAdapter($"SELECT * FROM {currentTableName}", connectionFrom);
                        SqlDataAdapter currentTableAdapterTo = new SqlDataAdapter($"SELECT * FROM {currentTableName}", connectionTo);

                        DataSet currentDataSet = new DataSet();
                        currentTableAdapterFrom.Fill(currentDataSet);
                        //currentTableAdapterTo.Update(currentDataSet);

                        //
                        System.Text.StringBuilder commandInsertBuilder = new System.Text.StringBuilder($"INSERT INTO {currentTableName}(");
                        System.Text.StringBuilder valueInsertBuilder = new System.Text.StringBuilder();

                        for (int i = 0; i < currentDataSet.Tables[0].Columns.Count; i++)
                        {
                            DataColumn column = currentDataSet.Tables[0].Columns[i];

                            if (column.AutoIncrement)
                                continue;

                            commandInsertBuilder.Append(column.ColumnName);
                            if (i < currentDataSet.Tables[0].Columns.Count - 1)
                            {
                                commandInsertBuilder.Append(", ");
                            }
                            else
                            {
                                commandInsertBuilder.Append(") VALUES(");
                            }
                        }

                        foreach (DataRow row in currentDataSet.Tables[0].Rows)
                        {
                            for (int i = 0; i < currentDataSet.Tables[0].Columns.Count; i++)
                            {
                                DataColumn column = currentDataSet.Tables[0].Columns[i];

                                if (column.AutoIncrement)
                                    continue;
                                
                                
                                valueInsertBuilder.Append(row[column]);
                                if (i < currentDataSet.Tables[0].Columns.Count - 1)
                                {
                                    valueInsertBuilder.Append(",");
                                }
                                else
                                {
                                    valueInsertBuilder.Append(");");
                                }
                            }
                            Console.WriteLine(commandInsertBuilder.ToString());
                            Console.WriteLine(valueInsertBuilder.ToString());
                        }
                        //

                        currentTableAdapterFrom = null;
                        currentTableAdapterTo = null;
                    }


                    connectionFrom.Close();
                    connectionTo.Close();
                }
                catch(Exception e)
                {

                }
            });
            
        }


    }
}