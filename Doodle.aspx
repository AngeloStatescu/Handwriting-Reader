<%@ Page Title="" Language="C#" MasterPageFile="~/HandwritingReader.Master" AutoEventWireup="true" CodeBehind="Doodle.aspx.cs" Inherits="HandwritingReader.Doodle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/jquery-3.2.1.min.js"></script>
    <link href="Content/styles.css" rel="stylesheet" />
    <script src="Scripts/canvas.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        #ContentPlaceHolder1_Button1 {
            font-size: 18px;
            width: 30%;
            float: left;
        }

        .inactiveButton {
            display: none;
        }

        #ContentPlaceHolder1_Panel1 {
            width: 100%;
            height: 500px;
            border: 2px solid white;
            overflow: auto;
        }
    </style>
    <div style="margin-top: 50px;">
        <div class="row" style="width: 990px !important; margin: 0 auto !important;">

            <div class="col-md-12 col-sm-12 col-xs-12 col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h1>Doodle</h1>
                    </div>

                    <div class="panel-body">

                        <div class="col-md-12 col-sm-12 col-xs-12 col-lg-12" style="font-size: 20px;">

                            <div id="wrapper">

                                <div id="blackboardPlaceholder">

                                    <canvas id="drawingCanvas" height="532" width="897">
                                        <p class="noscript">
                                            We're sorry, this web application is currently not supported with your browser. Please use an alternate browser or download a supported
                                                <br />
                                            browser. Supported browsers: <a href="http://www.google.com/chrome">Google Chrome</a>, <a href="http://www.opera.com">Opera</a>, <a href="http://www.mozilla.com">Firefox</a>, <a href="http://www.apple.com/safari">Safari</a>,
                                                <br />
                                            and <a href="http://www.konqueror.org">Konqueror</a>. Also make sure your JavaScript is enabled.
                                        </p>
                                    </canvas>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 col-sm-12 col-xs-12 col-lg-12" style="font-size: 20px; margin-top: 30px; text-align: center;">
                            <button type="button" class="btn btn-default" onclick="sendImage()" style="font-size: 18px; width: 30%; float: left;">
                                Generate Text</button>

                            <a class="btn btn-default" href="javascript:location.reload(true)" style="margin-bottom: 10px; float: right; font-size: 18px; width: 30%;">Clear Canvas
                            </a>
                        </div>
                        <div class="col-md-12 col-sm-12 col-xs-12 col-lg-12" style="font-size: 20px; margin-top: 60px; text-align: center;">
                             <asp:Button ID="Button1" runat="server" Text="Download Result" OnClick="Button2_Click" CssClass="btn btn-default inactiveButton"/>
                             <a id="downloadlink" style="display: none"></a>
                        </div>
                    </div>

                </div>
            </div>

        </div>
    </div>
</asp:Content>
