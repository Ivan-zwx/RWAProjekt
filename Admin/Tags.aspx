<%@ Page Title="" Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Tags.aspx.cs" Inherits="Admin.Tags" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div class="container">
        <fieldset class="p-4">
            <legend>Popis tagova</legend>
            <asp:Repeater ID="rptTags" runat="server">
                <HeaderTemplate>
                    <table id="myTable" class="table table-striped">
                        <thead>
                            <tr>
                                <th scope="col">#</th>
                                <th scope="col">Naziv</th>
                                <th scope="col">Kreirano</th>
                                <th scope="col">Tip</th>
                                <th hidden="hidden"></th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <th scope="row"><%# Eval("Id") %></th>
                        <td><%# Eval("Name") %></td>
                        <td><%# Eval("CreatedAt") %></td>
                        <td><%# Eval("Type") %></td>

                        <td>
                            <asp:LinkButton CssClass="btn btn-outline-danger" Visible='<%#(int)Eval("NumberOfApartments") == 0 %>' CommandArgument='<%# Eval("Id") %>' OnClick="btnDelete_Click" OnClientClick="return confirm('Jeste li sigurni da zelite obrisati tag?')" ID="btnDelete" runat="server">Obrisi</asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            <asp:Button ID="btnAddTag" OnClick="btnAddTag_Click" CssClass="btn btn-primary" runat="server" Text="Dodaj" />
        </fieldset>
    </div>
</asp:Content>
