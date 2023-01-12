<%@ Page Title="" Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="EditApartmentTags.aspx.cs" Inherits="Admin.EditApartmentTags" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div class="container p-5" style="display: flex; flex-direction: column;">
        <div class="row">
            <div class="col">
                <asp:Label runat="server" CssClass="form-label" for="txtName" Text="Naziv apartmana"></asp:Label>
                <asp:TextBox class="form-control" ID="txtName" runat="server" ReadOnly="true"></asp:TextBox>
            </div>
        </div>
    </div>
</asp:Content>