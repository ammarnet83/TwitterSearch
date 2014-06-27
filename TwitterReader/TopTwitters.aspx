<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="TopTwitters.aspx.cs" Inherits="TwitterReader.TopTwitters" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />
    
    <h3>Top Twitters</h3>
    <asp:Label ID="lblMessage" Text="" runat="server"></asp:Label>
    <ajaxToolkit:PieChart ID="pieChart1" runat="server" ChartHeight="400"
        ChartWidth="600" ChartTitle="Widget Production in the world"
        ChartTitleColor="#0E426C" Width="600px">
   
    </ajaxToolkit:PieChart>
</asp:Content>
