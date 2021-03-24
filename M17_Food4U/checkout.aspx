<%@ Page Title="" Language="C#" MasterPageFile="~/master_page.Master" AutoEventWireup="true" CodeBehind="checkout.aspx.cs" Inherits="M17_Food4U.checkout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-md">
        <h2 class="mt-5">Checkout</h2>

        <div class="p-5 border shadow-sm">
            <div id="div_produtos" runat="server">
            </div>
            <p class="w-100" style="display: flex; justify-content: flex-end" id="lb_total" runat="server" visible="false"><span class="mr-4">Total: </span> <b id="lb_precototal" runat="server">20€</b></p>
            <p id="lb_semprodutos" visible="false" runat="server">Sem produtos</p>
        </div>

        <h2 class="mt-5">Selecionar Morada</h2>

        <div class="p-5 border shadow-sm">
            <div id="div_moradas" runat="server">
                
            </div>
            <asp:Button Text="Adicionar Moradas" ID="btn_adicionarmoradas" Visible="false" PostBackUrl="~/user/perfil.aspx" CssClass="btn btn-outline-success" runat="server" />
        </div>
        <div class="d-flex justify-content-between mt-4 mb-5">
            <asp:Button Text="Voltar" CssClass="btn btn-secondary" PostBackUrl="~/index.aspx" runat="server" />
            <asp:Button Text="Confirmar" CssClass="btn btn-danger" ID="btn_confirmar" OnClick="btn_confirmar_Click" runat="server" />
        </div>

    </div>
    <script>
        function selectmorada(morada) {

            $(".fas.fa-check-circle").attr("hidden", "true")
            $(".far.fa-check-circle").removeAttr("hidden")

            
            $("#check_morada_" + morada).removeAttr("hidden")
            $("#uncheck_morada_" + morada).attr("hidden", "true")

            document.cookie = "morada=" + morada;
        }

        function AlterarQuantidade(user,menu,quantidade) {
            $.post("/servicos.asmx/AlterarQuantidadeMenu", {
                menu: menu,
                user: user,
                quantidade: quantidade
            });
            if (quantidade < 0) {
                $.toast({
                    text: "Produto removido com sucesso",
                    heading: 'Removido com sucesso',
                    icon: 'success',
                    showHideTransition: 'fade',
                    allowToastClose: false,
                    hideAfter: 1000,
                    stack: 10,
                    position: 'bottom-left',

                    textAlign: 'left',
                    loader: false,
                    loaderBg: '#9EC600',
                    beforeHide: function () {
                        location.reload();
                    }
                });
            }
            else {
                $.toast({
                    text: "Produto adicionado com sucesso",
                    heading: 'Adicionado com sucesso',
                    icon: 'success',
                    showHideTransition: 'fade',
                    allowToastClose: false,
                    hideAfter: 1000,
                    stack: 10,
                    position: 'bottom-left',

                    textAlign: 'left',
                    loader: false,
                    loaderBg: '#9EC600',
                    beforeHide: function () {
                        location.reload();
                    }
                });
            }
        }
    </script>
</asp:Content>
