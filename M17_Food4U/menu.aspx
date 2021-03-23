<%@ Page Title="" Language="C#" MasterPageFile="~/master_page.Master" AutoEventWireup="true" CodeBehind="menu.aspx.cs" Inherits="M17_Food4U.menu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="d-flex justify-content-between mt-5" id="page-detalhesmenu">
        <div class="w-50 mr-5" id="div_detalhesmenu">
            <div class="d-flex align-items-center justify-content-between">
                <h1 runat="server" id="menu_nome"></h1>
                <span class="" runat="server" id="menu_estrelas"></span>
            </div>
            
            <img src="" alt=""  runat="server" class="img-thumbnail w-100 shadow mb-2" id="menu_foto"/>
            <hr />
            <div class="bg-light p-3 mt-4 border mb-3">
                <div class="d-flex align-items-center mb-3">
                    <span class="mr-5 font-weight-bold">Preço:</span>
                    <span runat="server" id="menu_preco"></span>
                </div>
                <div class="d-flex align-items-center mb-3">
                    <span class="mr-5 font-weight-bold">Descrição:</span>
                    <span runat="server" id="menu_descricao"></span>
                </div>
                <div class="d-flex align-items-center">
                    <span class="mr-5 font-weight-bold">Restaurante:</span>
                    <span runat="server" id="menu_restaurante"></span>
                </div>
            </div>
            
            <asp:Button ID="btn_adicionarcarrinho" OnClick="btn_adicionarcarrinho_Click" CausesValidation="false" Text="Adicionar ao carrinho" CssClass="btn btn-lg btn-success" runat="server" />
        </div>
        <div class="w-50" id="div_comentarios">
            <h1>Comentários</h1>
            <div class="coment-zone border p-3">
                <div runat="server" id="div_inputcomentario">
                    <div class="mb-2">
                        <i class='far fa-star text-warning' id="star-1" style="cursor: pointer;" onclick="stars(1)"></i>
                        <i class='far fa-star text-warning' id="star-2" style="cursor: pointer;" onclick="stars(2)"></i>
                        <i class='far fa-star text-warning' id="star-3" style="cursor: pointer;" onclick="stars(3)"></i>
                        <i class='far fa-star text-warning' id="star-4" style="cursor: pointer;" onclick="stars(4)"></i>
                        <i class='far fa-star text-warning' id="star-5" style="cursor: pointer;" onclick="stars(5)"></i>
                    </div>
                    <asp:TextBox runat="server" id="txt_comentario" onclick="showButtons()" CssClass="form-control" placeholder="Comentário"/>
                    <div class="justify-content-end mt-2" id="botoes_comentar" hidden>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Campo obrigatório" BorderStyle="None" ControlToValidate="txt_comentario" CssClass="form-text text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:Label Text="" id="lb_erro_comentario" Visible="false" CssClass="form-text text-danger" runat="server" />
                        <asp:Button Text="Cancelar" CausesValidation="false" runat="server" ID="btn_cancelar" CssClass="btn btn-secondary mr-2"/>
                        <asp:Button Text="Comentar" ID="btn_comentar" OnClick="btn_comentar_Click" CssClass="btn btn-primary" runat="server" />
                    </div>
                </div>
                <div runat="server" visible="false" id="div_informarregisto">
                    Registe-se ou faça login para poder comentar
                </div>
                <div class="comentarios_content" id="div_comentarios_content" runat="server">
                    <!--<div class="comentario border p-3 bg-white mt-3">
                        <div class="d-flex align-items-center justify-content-between">
                            <span style="font-family: 'Roboto', sans-serif !important;  font-weight: bold; font-size: 18px;">Autor</span> 
                            <span><i class='fas fa-star text-warning'></i><i class='far fa-star text-warning'></i><i class='far fa-star text-warning'></i><i class='far fa-star text-warning'></i><i class='far fa-star text-warning'></i></span>
                        </div>
                        <small class='text-muted'>adasdads</small>
                        <br />
                        <span>Comentario</span>
                    </div> -->
                </div>
            </div>
            
        </div>
    </div>
    <script>
        $(document).ready(function () {
            document.cookie = "estrelas=; expires=Thu, 01 Jan 1970 00:00:00 UTC;";
        });

        function stars(num) {
            for (var i = 1; i < 6; i++) {
                if (i <= num)
                    $("#star-" + i).attr('class', 'fas fa-star text-warning');
                else
                    $("#star-" + i).attr('class', 'far fa-star text-warning');
            }
            document.cookie = "estrelas="+num;
        }

        function showButtons() {
            $('#botoes_comentar').removeAttr('hidden');
        }
    </script>
    

</asp:Content>
