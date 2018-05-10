<%@ Page Title="" Language="C#" MasterPageFile="~/HandwritingReader.Master" AutoEventWireup="true" CodeBehind="MyWritings.aspx.cs" Inherits="HandwritingReader.MyWritings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script>
        function downloadResult(response) {

            var nextSibling = response.nextSibling;
            while (nextSibling && nextSibling.nodeType != 1) {
                nextSibling = nextSibling.nextSibling
            }

            var textFile = null,
                makeTextFile = function (text) {
                    var data = new Blob([text], { type: 'text/plain' });

                    if (textFile !== null) {
                        window.URL.revokeObjectURL(textFile);
                    }

                    textFile = window.URL.createObjectURL(data);

                    return textFile;
                };


            var textbox = nextSibling.innerText;

            var link = document.getElementById('downloadlink');
            link.href = makeTextFile(textbox);
            link.download = "Result " + new Date().toLocaleString() + ".txt"
            link.click();

        }
    </script>
    <div style="margin-left: 25%; margin-right: 25%; margin-top: 50px;">
        <a id="downloadlink" style="display: none"></a>
        <div class="row" id="writingsContainer" runat="server">

        </div>
    </div>
</asp:Content>
