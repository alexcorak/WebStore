<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="focus.aspx.cs" Inherits="focus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        #scrollHere
        {
            
        }
        .class
        {
            width:50%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    

    <div id="scrollHere" class="container">
        <asp:Image ID="Image1" runat="server" CssClass="class"/> 
        <asp:Label ID="Label1" runat="server" Text="" BorderStyle="Solid" BorderColor="RoyalBlue"></asp:Label>
        <asp:Button ID="Button2" runat="server" Text="Back to Shopping" onClick ="backButton"/>
        <asp:Button ID="Button1" runat="server" Text="Add to Cart" onClick ="addCart"/>
        <asp:Image ID="Image2" runat="server"/>
    </div>
    
    
</asp:Content>

