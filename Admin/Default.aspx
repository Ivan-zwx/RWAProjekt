<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Admin.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div class="container py-4">
        <!-- PANEL PORUKA -->
        <asp:Panel ID="PanelIspis" CssClass="container mt-5" runat="server" Visible="False">
            <div class='alert alert-danger' role='alert'>
                <asp:Label ID="lblErrorLogin" meta:resourcekey="lblErrorLogin" runat="server" Text="Check the entered data again!"></asp:Label>
            </div>
        </asp:Panel>
        <!-- // -->

        <asp:Panel ID="PanelForma" runat="server" Visible="True">
            <!-- FORM -->
            <fieldset class="p-4">
                <legend runat="server" meta:resourcekey="legendLogin">Prijava</legend>
                <div class="mb-3">
                    <asp:Label ID="lblUserName" meta:resourcekey="lblUserName" class="form-label" runat="server" Text="Korisničko ime"></asp:Label>
                    <asp:TextBox ID="txtUserName" class="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" meta:resourcekey="rfvUserName" runat="server" ControlToValidate="txtUserName" Display="Dynamic" ForeColor="Red">* Molim unesite korisničko ime</asp:RequiredFieldValidator>
                </div>
                <div class="mb-3">
                    <asp:Label ID="lblPassword" meta:resourcekey="lblPassword" class="form-label" runat="server" Text="Lozinka"></asp:Label>
                    <asp:TextBox ID="txtPassword" TextMode="Password" class="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" meta:resourcekey="rfvPassword" runat="server" ControlToValidate="txtPassword" Display="Dynamic" ForeColor="Red">* Molim unesite lozinku</asp:RequiredFieldValidator>
                </div>
                <asp:Button ID="btnLogin" meta:resourcekey="btnLogin" class="btn btn-primary" runat="server" Text="Prijava" OnClick="btnLogin_Click" />
            </fieldset>
            <!-- // -->
        </asp:Panel>
    </div>
</asp:Content>
