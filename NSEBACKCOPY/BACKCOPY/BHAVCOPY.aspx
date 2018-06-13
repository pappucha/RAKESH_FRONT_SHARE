<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BHAVCOPY.aspx.cs" Inherits="NSEBACKCOPY.BACKCOPY.BHAVCOPY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>

        <asp:Button Text="BtnDownload" ID="btnDownload" runat="server" OnClick="btnDownload_Click" />


        <asp:Label ID="lblerror" runat="server"></asp:Label>
    
    </div>
    </form>
</body>
</html>
