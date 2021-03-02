<%@ Page Title="" Language="C#" MasterPageFile="~/master_page.Master" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="M17_Food4U.register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://www.google.com/recaptcha/api.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h2 class="mt-5">Registar</h2>

        <div class="form-group mt-4" errormessage="Email inválido">
            <label for="ContentPlaceHolder1_txt_email" controltovalidate="txt_email">Email</label>
            <asp:TextBox TextMode="Email" class="form-control" placeholder="Email" ID="txt_email" runat="server" />
            <asp:RequiredFieldValidator ValidationGroup='val_register' ID="RequiredFieldValidator1" runat="server" ErrorMessage="Campo Obrigratório" ControlToValidate="txt_email" CssClass="form-text text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ValidationGroup='val_register' ID="RegularExpressionValidator1" runat="server" ErrorMessage="Email Inválido" ControlToValidate="txt_email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" CssClass="form-text text-danger" Display="Dynamic"></asp:RegularExpressionValidator>
        </div>
        <div class="form-group">
            <label for="ContentPlaceHolder1_txt_nome">Nome</label>
            <asp:TextBox class="form-control" placeholder="Primeiro Ultimo" ID="txt_nome" runat="server" />
            <asp:RequiredFieldValidator ValidationGroup='val_register' ID="RequiredFieldValidator6" runat="server" ErrorMessage="Campo Obrigratório" ControlToValidate="txt_nome" CssClass="form-text text-danger" Display="Dynamic"></asp:RequiredFieldValidator>

        </div>
        <div class="form-group">
            <label for="ContentPlaceHolder1_txt_nif">NIF</label>
            <asp:TextBox class="form-control" ID="txt_nif" runat="server" />
            <asp:RequiredFieldValidator ValidationGroup='val_register' ID="RequiredFieldValidator5" runat="server" ErrorMessage="Campo Obrigratório" ControlToValidate="txt_nif" CssClass="form-text text-danger" Display="Dynamic"></asp:RequiredFieldValidator>

        </div>
        <div class="form-group">
            <label for="ContentPlaceHolder1_txt_data_nasc">Data de Nascimento</label>
            <asp:TextBox class="form-control" TextMode="Date" ID="txt_data_nasc" runat="server" />
            <asp:RequiredFieldValidator ValidationGroup='val_register' ID="RequiredFieldValidator4" runat="server" ErrorMessage="Campo Obrigratório" ControlToValidate="txt_data_nasc" CssClass="form-text text-danger" Display="Dynamic"></asp:RequiredFieldValidator>

        </div>

        <div class="form-group">
            <label for="ContentPlaceHolder1_txt_password">Password</label>
            <asp:TextBox TextMode="Password" class="form-control" ID="txt_password" placeholder="Password" runat="server" />
            <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Password tem de ter 5 caracteres" Display="Dynamic" CssClass="form-text text-danger" ControlToValidate="txt_password" ValidateEmptyText="True" ValidationGroup="val_register" ClientValidationFunction="validateRegisterPassword"></asp:CustomValidator>
            
        </div>
        <div class="form-group">
            <label for="ContentPlaceHolder1_txt_conf_password">Confirmar Password</label>
            <asp:TextBox TextMode="Password" class="form-control" ID="txt_conf_password" placeholder="Confirmar Password" runat="server" />
            <asp:RequiredFieldValidator ValidationGroup='val_register' ID="RequiredFieldValidator3" runat="server" ErrorMessage="Campo Obrigratório" ControlToValidate="txt_conf_password" CssClass="form-text text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:CompareValidator ValidationGroup='val_register' ID="CompareValidator1" runat="server" ErrorMessage="Passwords diferentes" ControlToCompare="txt_password" ControlToValidate="txt_conf_password" Display="Dynamic" CssClass="form-text text-danger"></asp:CompareValidator>
        </div>

        <div class="form-group">
            <label for="ContentPlaceHolder1_dp_perfis">Tipo de Utilizador</label>
            <asp:DropDownList class="form-control alert-warning" ID="dp_perfis" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dp_perfis_SelectedIndexChanged">
                <asp:ListItem Value="3" Text="Utilizador" />
                <asp:ListItem Value="2" Text="Estafeta" />
                <asp:ListItem Value="1" Text="Restaurante" />
            </asp:DropDownList>
        </div>

        <div runat="server" id="frm_estafeta" visible="false">
            <div class="form-group">
                <label for="ContentPlaceHolder1_txt_carta_conducao" class="text-warning">Carta de condução</label>
                <asp:TextBox class="form-control" ID="txt_carta_conducao" runat="server" placeholder="Carta de condução" />
            </div>
            <div class="form-group">
                <label for="ContentPlaceHolder1_txt_validade_carta_conducao" class="text-warning">Validade Carta de condução</label>
                <asp:TextBox class="form-control" TextMode="Date" ID="txt_validade_carta_conducao" runat="server" />
            </div>
        </div>

        <div runat="server" id="frm_restaurante" visible="false">
            <div class="form-group">
                <label for="ContentPlaceHolder1_txt_nome_restaurante" class="text-warning">Nome do Restaurante</label>
                <asp:TextBox class="form-control" ID="txt_nome_restaurante" runat="server" placeholder="Nome do Restaurante" />
            </div>
            <div class="form-group">
                <label for="ContentPlaceHolder1_txt_cidade" class="text-warning">Cidade</label>
                <asp:TextBox class="form-control" ID="txt_cidade" runat="server" placeholder="Cidade" />
            </div>
            <div class="form-group">
                <label for="ContentPlaceHolder1_txt_cp" class="text-warning">Código Postal</label>
                <asp:TextBox class="form-control" ID="txt_cp" runat="server" placeholder="Código Postal" />
            </div>
            <div class="form-group">
                <label for="ContentPlaceHolder1_txt_morada" class="text-warning">Morada</label>
                <asp:TextBox class="form-control" ID="txt_morada" runat="server" placeholder="Morada" />
            </div>
            <div class="form-group">
                <label for="ContentPlaceHolder1_FileUpload1" class="text-warning">Foto do restaurante</label>

                <asp:FileUpload CssClass="form-control-file" ID="FileUpload1" runat="server" />
            </div>
        </div>
        <div class="alert alert-danger" visible="false" id="div_erro" runat="server">
            <asp:Label ID="lb_erro"  Text="" runat="server" />
        </div>

        <div class="g-recaptcha" data-sitekey="6Ldg9moaAAAAAHKq47HPojxcYrVCbFBwMIgUsZsd"></div>

        <asp:Button ValidationGroup='val_register' class="btn-lg btn-primary" runat="server" Text="Registar" ID="btn_registar"  OnClick="btn_registar_Click" />
    </div>
    <script>
        function validateRegisterPassword(sender, args) {
            if (args.Value.length < 5)
                args.IsValid = false;
            else
                args.IsValid = true;
        }
    </script>
</asp:Content>
