<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TweetsMap.aspx.cs" Inherits="TwitterReader.TweetsMap" Title="Hello Twitter Reader - Map" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Css/Style.css" rel="stylesheet" />
</head>
<body onload="setupMap()" onunload="GUnload()">
    <form id="form1" runat="server">
        <div>
            <h3>Map Locations for Tweets</h3>
            <script src="http://maps.google.com/maps?file=api&amp;v=2&amp;key=ABQIAAAA-O3c-Om9OcvXMOJXreXHAxQGj0PqsCtxKvarsoS-iqLdqZSKfxS27kJqGZajBjvuzOBLizi931BUow"
                type="text/javascript"></script>
            <script type="text/javascript">
                document.write('<script type="text/javascript" src="http://gmaps-utility-library.googlecode.com/svn/trunk/labeledmarker/1.4/src/labeledmarker' + (document.location.search.indexOf('packed') > -1 ? '_packed' : '') + '.js"><' + '/script>');
            </script>
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
            <div id="map" style="margin: 5px auto; width: 100%; height: 600px"></div>
        </div>
    </form>
</body>
</html>
