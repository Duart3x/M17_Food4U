<%@ Page Title="" Language="C#" MasterPageFile="~/master_page.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="M17_Food4U.login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="login_container">
        <div class="login_col">
            <h2>Login</h2>
            <div class="w-50">
                <div class="form-group mt-4" errormessage="Email inválido">
                    <label for="ContentPlaceHolder1_txt_email" controltovalidate="txt_email">Email</label>
                    <asp:TextBox TextMode="Email" class="form-control" placeholder="Email" ID="txt_email" runat="server" />
                    <asp:RequiredFieldValidator ValidationGroup='val_login' ID="RequiredFieldValidator1" runat="server" ErrorMessage="Campo Obrigratório" ControlToValidate="txt_email" CssClass="form-text text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ValidationGroup='val_login' ID="RegularExpressionValidator1" runat="server" ErrorMessage="Email Inválido" ControlToValidate="txt_email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" CssClass="form-text text-danger" Display="Dynamic"></asp:RegularExpressionValidator>
                </div>
                <div class="form-group w-100">
                    <label for="ContentPlaceHolder1_txt_password">Password</label>
                    <asp:TextBox TextMode="Password" class="form-control" ID="txt_password" placeholder="Password" runat="server" />
                    <asp:RequiredFieldValidator ValidationGroup='val_login' ID="RequiredFieldValidator2" runat="server" ErrorMessage="Campo Obrigratório" ControlToValidate="txt_password" CssClass="form-text text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
                <div class="alert alert-danger" visible="false" id="div_erro" runat="server">
                    <asp:Label ID="lb_erro"  Text="" runat="server" />
                </div>
                <asp:Button ValidationGroup='val_login' Text="LOGIN" CssClass="btn btn-block btn-lg btn-primary" ID="btn_login" OnClick="btn_login_Click" runat="server" />
            </div>
        </div>
        <span class="login_divisor"></span>
        <div class="login_col">
            <div class="w-50" style="display: flex; flex-direction: column;">
                <h2>Ainda não tem uma conta?</h2>
                <ul class="login_list mt-4 mb-5">
                    <li class="mb-3">Encomende já a sua próxima refeição!</li>
                    <li class="mb-3">Acompanhe os seus pedidos!</li>
                    <li>Guarde os seus detalhes de pagamento e de envio e poupe tempo!</li>
                </ul>
                <a href="/register.aspx" class="btn btn-block btn-lg btn-primary">CRIAR CONTA</a>
            </div>
            
        </div>
    </div>
</asp:Content>
