<%@ Page Title="" EnableEventValidation="false" EnableViewState="false" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="cspad.web.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


<div id="instr" class="sprite"></div>

<div id="content">
    <div id="right-column"><div>
        <h2 id="logo" class="sprite"><a href="/" title="C# pastebin and code snippets">cspad - C# pastebin and code snippets</a></h2>

        <div id="recess" class="sprite sprite-recess">
            <input readonly="readonly" type="text" ID="ShareLink" runat="server"/> 
        </div><br />

        <div style="margin-top: 10px; margin-bottom: 20px;">
            <a class="sprite sprite-twitter share-link" id="TwitterShareLink" title="Share link on Twitter" target="_blank" runat="server"></a
            ><a class="sprite sprite-facebook share-link" id="FacebookShareLink" title="Share link on Facebook" target="_blank" runat="server"></a>
                <a id="ViewRawLink" runat="server" target="_blank">view raw</a>
        </div>

        <div style="margin-bottom: 20px;">
            <span style="margin-right: 20px;" id="CreatedTime" runat="server"></span>
            <span id="ViewCount" runat="server"></span><br />
        </div>

        <asp:Panel ID="SaveContainer" runat="server">
            <a id="save-btn" class="sprite"></a>        
            <div id="lastSaved"></div>
        </asp:Panel>        
        
        <asp:Panel ID="CloneContainer" runat="server" Visible="false">        
            <a id="clone-btn" class="sprite"></a>   
        </asp:Panel>     
        
      </div>
    </div>

</div>

    <div id="code-cont">
<textarea id="code" name="code" class="fill"><asp:Literal ID="CodeLiteral" runat="server"></asp:Literal></textarea>
    </div>


</asp:Content>
