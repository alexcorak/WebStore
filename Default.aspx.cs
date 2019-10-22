using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        List<Product> list = new List<Product>();
        string code1="";
        SqlCommand cmd;
        SqlConnection cn;
        cn = new SqlConnection();
        cn.ConnectionString = "Data Source=sql7002.site4now.net;" +
    "Initial Catalog=DB_A445E8_salesAdmin;" +
    "MultipleActiveResultSets = true;" +
    "User ID=DB_A445E8_salesAdmin_admin;" +
    "Password=warcraft3";

        cmd = new SqlCommand("select * from Inventory",cn);
        cn.Open();
        SqlDataReader dr = cmd.ExecuteReader();

        while (dr.Read())
        {
            decimal price = decimal.Parse(dr["UnitPrice"].ToString());
            Product product = new Product(price, float.Parse(dr["UnitWeight"].ToString()), dr["UrlFull"].ToString(),dr["ItemNumber"].ToString(),dr["Description"].ToString(),dr["Marketing"].ToString());
            list.Add(product);
        }
        code1 += "<div class=\"row\">";
        foreach (Product p in list) {
            code1 +=
               "<div class=\"col-lg-4 col-md-6 mb-4\">" +
                 "<div class=\"card h-100\">" +
                   "<a href= \"" + onClick(p) + "\">" +
                   "<img class=\"card-img-top\" src=" + "\"" + p.UrlFull + "\"/>" +
               "<div class=\"card-body\">" +
                 "<h4 class=\"card-title\">" +
                   "<a href = \"#\" > " + p.name + "</a>" +
                 "</h4>" +
                 "<h5> $" + p.price + "</h5>" +
               "</div>" +
               "<div class=\"card-footer\">" +
                 "<small class=\"text-muted\">&#9733; &#9733; &#9733; &#9733; &#9734;</small>" +
               "</div>" +
             "</div>";
            code1 += "</div>";
        }
        this.Item1.Text = code1;
    }

    protected string onClick(Product p)
    {
        string url = "focus.aspx?";
        url += "ItemNumber=" + p.name+Server.UrlEncode(p.itemNumber);
        return url;
    }
}