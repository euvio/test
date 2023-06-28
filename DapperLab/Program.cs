// See https://aka.ms/new-console-template for more information

using Dapper;
using Npgsql;
using System.Data;

internal class Program
{
    public static async Task Main(string[] args)
    {
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

        IDbConnection connection = new NpgsqlConnection();
        connection.ConnectionString = "server=localhost;uid=postgres;password=123456;database=dapper";

        string sql = """
            SELECT
            	"id",
            	"name",
                "age",
            	home_addr
            FROM
            	Student;
            """;

        var ss = connection.Query<Student>(sql);
        foreach (var student in ss)
        {
            Console.WriteLine(student.ToString());
        }

        var sss = (await connection.QueryAsync<Student>(sql).ConfigureAwait(false));
        foreach (var student in sss)
        {
            Console.WriteLine(student.ToString());
        }


        string sql2 = """
                        SELECT
            	"id",
            	"name",
            	age,
            	home_addr 
            FROM
            	student 
            WHERE
            	"home_addr" = @homeaddr;
            """;
        var ss2 = connection.QuerySingle<Student>( sql2,new { homeaddr = "纽约"});

    }
}

public class Student
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Age { get; set; }
    public string? HomeAddr { get; set; }

    public override string ToString()
    {
        return $"{Id},{Name},{Age},{HomeAddr}";
    }
}