<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KertotauluForm.aspx.cs" Inherits="Kertotaulu.KertotauluForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>MustGet Kertolaskut</title>
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <h1>Harjoittele 1-10 kertotauluja</h1>
        
        <asp:Label ID="viestiLabel" runat="server"></asp:Label>
        <br />
        <br />
        <asp:Label ID="kysymysLabel" runat="server"></asp:Label>
        <br />
        <br />
        <asp:TextBox ID="vastausKentta" runat="server" Height="16px" Width="128px" autocomplete="off" onkeydown = "return (!((event.keyCode>=65 && event.keyCode <= 95) || event.keyCode >= 106 || (event.keyCode >= 48 && event.keyCode <= 57 && isNaN(event.key))) && event.keyCode!=32);"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;
        <br />
        <br />
        <asp:Label ID="pisteLabel" runat="server" Text="Pisteet:" Visible="False"></asp:Label>
        <br />
        <br />

        <asp:Button ID="vastaaButton" runat="server" OnClick="vastaaButton_Click" Text="Vastaa" CssClass ="button"/>
        <asp:Button ID="seuraavaButton" runat="server" OnClick="seuraavaButton_Click" Text="Seuraava" CssClass ="button" />
        <asp:Button ID="naytaPisteetButton" runat="server" OnClick="naytaPisteetButton_Click" Text="Näytä pisteet" CssClass ="button"/>
        <asp:Button ID="piilotaPisteetButton" runat="server" OnClick="piilotaPisteetButton_Click" Text="Piilota pisteet" Visible="False" CssClass ="button" />
        <asp:Button ID="alustaButton" runat="server" Text="Aloita alusta" OnClick="alustaButton_Click" CssClass ="button" />

    </form>
</body>
</html>
