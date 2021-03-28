<%@ Page Title="" Language="C#" MasterPageFile="~/master_page.Master" AutoEventWireup="true" CodeBehind="perfil.aspx.cs" Inherits="M17_Food4U.user.perfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="https://www.paypal.com/sdk/js?client-id=ASaZ58lghk2jKCtklnQccMKq8sQTSMC3hEol3ZZq79nVHlpXQqP1owoNBVfQRnX0JKA0HIf3gsEOumoi&currency=EUR&disable-funding=credit,card&locale=pt_PT"></script>

    <div class="mt-5 d-flex align-items-center justify-content-between">
        <div>
            <h2>Perfil</h2>
            <p class="text-muted">Edite o seu perfil</p>
        </div>
        
        <asp:Button Text="Logout" CssClass="btn btn-lg btn-outline-danger" runat="server" ID="btn_logout" OnClick="btn_logout_Click" />
    </div>
    <div class="w-100 border p-3">
        <div class="d-flex align-items-center justify-content-between mb-3">
            <label for="ContentPlaceHolder1_txt_email" class="font-weight-bold mb-0" runat="server">Email:</label>
            <asp:TextBox runat="server" ID="txt_email" value="email@gmail.com" placeholder="email" CssClass="form-control-plaintext w-75" />
        </div>
        <div class="d-flex align-items-center justify-content-between mb-3">
            <label for="ContentPlaceHolder1_txt_nome" class="font-weight-bold mb-0" runat="server">Nome:</label>
            <asp:TextBox runat="server" ID="txt_nome" value="Duarte Santos" placeholder="Nome Completo" CssClass="form-control-plaintext w-75" />
        </div>
        <div class="d-flex align-items-center justify-content-between mb-3">
            <label for="ContentPlaceHolder1_txt_nif" class="font-weight-bold" runat="server">NIF:</label>
            <asp:TextBox runat="server" ID="txt_nif" value="235987654" placeholder="NIF" CssClass="form-control-plaintext w-75" />
        </div>
        <div class="d-flex align-items-center justify-content-between mb-3">
            <label for="ContentPlaceHolder1_txt_data_nasc" class="font-weight-bold mb-0" runat="server">Data Nascimento:</label>
            <asp:TextBox runat="server" ID="txt_data_nasc" value="2003/05/13" TextMode="Date" placeholder="Data de nascimento" CssClass="form-control-plaintext w-75" />
        </div>

        <div class="d-flex align-items-center justify-content-between">
            <label for="ContentPlaceHolder1_txt_newpassowrd" class="font-weight-bold mb-0" runat="server">Password:</label>
            <asp:TextBox runat="server" ID="txt_newpassowrd"  TextMode="Password" placeholder="Palavra Passe nova" CssClass="form-control-plaintext w-75" />
        </div>
        <div class="d-flex flex-row-reverse align-items-center mt-3">
            <asp:Button Text="Editar" ID="btn_editar" OnClick="btn_editar_Click" CssClass="btn btn-primary" runat="server" />
            <asp:Button Text="Confirmar" ID="btn_confirmar" OnClick="btn_confirmar_Click" CssClass="btn btn-outline-success " runat="server" Visible="false" />
            <asp:Button Text="Cancelar" ID="btn_cancelar" OnClick="btn_cancelar_Click" CssClass="btn btn-outline-secondary mr-2" runat="server" Visible="false" />
            <div class="alert alert-danger mr-2" visible="false" id="div_erro2" runat="server">
                <asp:Label ID="lb_erro2" Text="" runat="server" />
            </div>
        </div>
    </div>

    <h2 class="mt-4">Saldo</h2>
    <p class="text-muted">Levante ou deposite dinheiro no site usando PayPal</p>
    <div class="d-flex w-100 border  p-3">
        <div class="d-flex w-50 flex-column">
            <div class="d-flex flex-column w-100">
                <label for="ContentPlaceHolder1_txt_deposito" class="font-weight-bold mb-0" runat="server">Depositar:</label>
                <asp:TextBox runat="server" ID="txt_deposito" CssClass="form-control" placeholder="Valor a depositar" />
            </div>
            <div id="btns_paypal" class="w-25 mt-4">

            </div>
        </div>

        <div class="d-flex w-50 ml-5 flex-column">
            <div class="w-100">
                <asp:Label Text="Levantar" runat="server" />
                <asp:TextBox runat="server" ID="txt_emaillevantar" CssClass="form-control" placeholder="Email paypal para levantar"/>
                <asp:TextBox runat="server" ID="txt_moneylevantar" CssClass="form-control mt-4" placeholder="Valor a levantar" />
            </div>
            <div>
                <asp:Button Text="Levantar Saldo" CssClass="btn btn-lg btn-outline-success mt-2" id="btn_levantarsaldo" OnClick="btn_levantarsaldo_Click" runat="server" />
            </div>
        </div>

    </div>

    <div class="mt-4">
        <h2>Moradas</h2>
        <p class="text-muted">Edite as suas moradas</p>

        <div class="w-100 border p-3">
            <div>
                <asp:TextBox runat="server" ID="txt_address1" value="" placeholder="Morada" CssClass="form-control mb-1" />
                <asp:TextBox runat="server" ID="txt_cidade1" value="" placeholder="Cidade" CssClass="form-control mb-1" />
                <asp:TextBox runat="server" ID="txt_cp1" value="" placeholder="Código Postal" CssClass="form-control" />

                <div class="d-flex align-items-center mt-3">
                    <asp:Button Text="Editar" ID="btn_editar_morada1" OnClick="btn_editar_morada1_Click" CssClass="btn btn-primary mr-2" runat="server" Visible="false" />
                    <asp:Button Text="Adicionar Morada" ID="btn_adicionar_morada1" OnClick="btn_adicionar_morada1_Click" CssClass="btn btn-outline-success " runat="server" />
                    <asp:Button Text="Cancelar" ID="btn_cancelar_morada1" OnClick="btn_cancelar_morada1_Click" CssClass="btn btn-outline-secondary mr-2" runat="server" Visible="false" />
                    <asp:Button Text="Confirmar" ID="btn_confirmar_morada1" OnClick="btn_confirmar_morada1_Click" CssClass="btn btn-outline-success " runat="server" Visible="false" />
                    <div class="alert alert-danger mr-2" visible="false" id="div_erro_morada1" runat="server">
                        <asp:Label ID="lb_erro_morada1" Text="" runat="server" />
                    </div>
                </div>
            </div>
            <hr />
            <div>
                <asp:TextBox runat="server" ID="txt_address2" value="" placeholder="Morada" CssClass="form-control mb-1" />
                <asp:TextBox runat="server" ID="txt_cidade2" value="" placeholder="Cidade" CssClass="form-control mb-1" />
                <asp:TextBox runat="server" ID="txt_cp2" value="" placeholder="Código Postal" CssClass="form-control" />

                <div class="d-flex align-items-center mt-3">
                    <asp:Button Text="Editar" ID="btn_editar_morada2" OnClick="btn_editar_morada2_Click" CssClass="btn btn-primary mr-2" runat="server" Visible="false" />
                    <asp:Button Text="Adicionar Morada" ID="btn_adicionar_morada2" OnClick="btn_adicionar_morada2_Click" CssClass="btn btn-outline-success " runat="server" />
                    <asp:Button Text="Cancelar" ID="btn_cancelar_morada2" OnClick="btn_cancelar_morada2_Click" CssClass="btn btn-outline-secondary mr-2" runat="server" Visible="false" />
                    <asp:Button Text="Confirmar" ID="btn_confirmar_morada2" OnClick="btn_confirmar_morada2_Click" CssClass="btn btn-outline-success " runat="server" Visible="false" />
                    <div class="alert alert-danger mr-2" visible="false" id="div_erro_morada2" runat="server">
                        <asp:Label ID="lb_erro_morada2" Text="" runat="server" />
                    </div>
                </div>
            </div>
            <hr />
            <div>
                <asp:TextBox runat="server" ID="txt_address3" value="" placeholder="Morada" CssClass="form-control mb-1" />
                <asp:TextBox runat="server" ID="txt_cidade3" value="" placeholder="Cidade" CssClass="form-control mb-1" />
                <asp:TextBox runat="server" ID="txt_cp3" value="" placeholder="Código Postal" CssClass="form-control" />

                <div class="d-flex align-items-center mt-3">
                    <asp:Button Text="Editar" ID="btn_editar_morada3" OnClick="btn_editar_morada3_Click" CssClass="btn btn-primary mr-2" runat="server" Visible="false" />
                    <asp:Button Text="Adicionar Morada" ID="btn_adicionar_morada3" OnClick="btn_adicionar_morada3_Click" CssClass="btn btn-outline-success" runat="server" />
                    <asp:Button Text="Cancelar" ID="btn_cancelar_morada3" OnClick="btn_cancelar_morada3_Click" CssClass="btn btn-outline-secondary mr-2" runat="server" Visible="false" />
                    <asp:Button Text="Confirmar" ID="btn_confirmar_morada3" OnClick="btn_confirmar_morada3_Click" CssClass="btn btn-outline-success " runat="server" Visible="false" />
                    <div class="alert alert-danger mr-2" visible="false" id="div_erro_morada3" runat="server">
                        <asp:Label ID="lb_erro_morada3" Text="" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="mt-4">
        <h2>Transações</h2>
        <p class="text-muted">Veja o histório de depósitos e levantamentos</p>

        <asp:GridView runat="server" EmptyDataText="Sem Transações" CssClass="table" ID="dgv_transacoes" HeaderStyle-BackColor="#212529" HeaderStyle-ForeColor="White"></asp:GridView>
    </div>

    <div class="mt-4">
        <h2>Pagamentos</h2>
        <p class="text-muted">Veja o histório de pagamentos</p>

        <asp:GridView runat="server" EmptyDataText="Sem Pagamentos" CssClass="table" ID="dgv_pagamentos" HeaderStyle-BackColor="#212529" HeaderStyle-ForeColor="White"></asp:GridView>
    </div>

    <div class="mt-4 mb-5">
        <h2>Pedidos</h2>
        <p class="text-muted">Veja o histório de compras</p>

        <asp:GridView runat="server" EmptyDataText="Sem Pedidos" CssClass="table" ID="dgv_pedidos" HeaderStyle-BackColor="#212529" HeaderStyle-ForeColor="White"></asp:GridView>
    </div>

    <div class="modal fade" id="modal-confirm-password" tabindex="-1" aria-labelledby="ModalPassword" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="ModalPassword">Confirmar Password Atual</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body" id="modal_body2">
                    <div class="form-group">
                        <label for="ContentPlaceHolder1_txt_password" runat="server">Password Atual:</label>
                        <asp:TextBox runat="server" ID="txt_password" TextMode="Password" placeholder="Password Atual" CssClass="form-control" />
                    </div>
                    <div class="alert alert-danger" visible="false" id="div_erro" runat="server">
                        <asp:Label ID="lb_erro" Text="" runat="server" />
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                    <button type="button" runat="server" onserverclick="btn_ConfirmarPassword_ServerClick" id="btn_ConfirmarPassword" class="btn btn-success">Confirmar</button>
                </div>
            </div>
        </div>
    </div>
    <script>
        function openConfirmPassword() {
            $('#modal-confirm-password').modal('show')
        }
    </script>

    <script>
        paypal.Buttons({
            createOrder: function (data, actions) {
                return actions.order.create({
                    purchase_units: [{
                        amount: {
                            value: $("#ContentPlaceHolder1_txt_deposito").val().replace(",",".")
                        }
                    }]
                });
            },
            onApprove: function (data, actions) {
                return actions.order.capture().then(function (details) {
                    $.post("/servicos.asmx/DepositarDinheiro", {
                        valor: details.purchase_units[0].amount.value
                    }, function (result) {
                        location.reload()
                    });
                });
            }
        }).render('#btns_paypal');
    </script>
</asp:Content>
