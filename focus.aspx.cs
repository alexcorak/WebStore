using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class focus : System.Web.UI.Page
{
    private string selected;
    private List<cartProduct> cart = new List<cartProduct>();
    private cartProduct cartItem;

    protected void Page_Load(object sender, EventArgs e)
    {
        string item = Request.QueryString["ItemNumber"];
        item= item[item.Length - 1].ToString();
        SqlCommand cmd;
        SqlConnection cn;
        cn = new SqlConnection();
        if(Session["MyCart"]!=null)
            cart = (List<cartProduct>)Session["MyCart"];

        //string a = WebConfigurationManager.ConnectionStrings["Content"].ToString();
        cn.ConnectionString = "Data Source=sql7002.site4now.net;" +
    "Initial Catalog=DB_A445E8_salesAdmin;" +
    "MultipleActiveResultSets = true;" +
    "User ID=DB_A445E8_salesAdmin_admin;" +
    "Password=warcraft3";

        cmd = new SqlCommand("select * from Inventory where ItemNumber = '" + item+"'", cn);
        cn.Open();
        SqlDataReader dr1 = cmd.ExecuteReader();

        while (dr1.Read())
        {
            string display = dr1["Marketing"].ToString();
            Product p = new Product(decimal.Parse(dr1["UnitPrice"].ToString()), float.Parse(dr1["UnitWeight"].ToString()), dr1["UrlFull"].ToString(), dr1["ItemNumber"].ToString(), dr1["Description"].ToString(), dr1["Marketing"].ToString());
            Label1.Text = p.name + "<br />"
                +p.description +"<br />"
                +"$"+p.price;
            selected = dr1["ItemNumber"].ToString();
            Image1.ImageUrl = dr1["UrlFull"].ToString();
        }
    }

    protected void addCart(object sender, EventArgs e)
    {
        Boolean found = false;
        cartItem = new cartProduct(1, int.Parse(this.selected));
        if (cart.Count > 0)
        {
            foreach (cartProduct p in cart)
            {
                if (p.getItemNumber() == cartItem.getItemNumber())
                {
                    cartItem.setQuantity(cartItem.getQuantity()+1);
                    found = true;
                }
            }
        }
        if (!found)
        {
            cartItem = new cartProduct(1, int.Parse(this.selected));
            cartItem.setQuantity(1);
            cart.Add(cartItem);
            Session["MyCart"] = cart;
        }
        this.Label1.Text = "Added to Cart!";
    }

    protected void backButton(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx#container");
    }
}