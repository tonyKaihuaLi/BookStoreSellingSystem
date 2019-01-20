<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="ImageUpload.aspx.cs" Inherits="Web.ImageUpload" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
    <script src="js/jquery-1.7.1.js"></script>
    <script src="SWFUpload/handlers.js"></script>
    <script src="SWFUpload/swfupload.js"></script>
    <script src="js/jquery.imgareaselect.min.js"></script>
    <script type="text/javascript">
        var swfu;
        window.onload = function () {
            swfu = new SWFUpload({
                // Backend Settings
                upload_url: "/ashx/upload.ashx",
                post_params: {
                    "ASPSESSID": "<%=Session.SessionID %>"
                },

			    // File Upload Settings
			    file_size_limit: "2 MB",
			    file_types: "*.jpg;*.gif",
			    file_types_description: "JPG Images",
			    file_upload_limit: 0,    // Zero means unlimited

			    // Event Handler Settings - these functions as defined in Handlers.js
			    //  The handlers are not part of SWFUpload but are part of my website and control how
			    //  my website reacts to the SWFUpload events.
			    swfupload_preload_handler: preLoad,
			    swfupload_load_failed_handler: loadFailed,
			    file_queue_error_handler: fileQueueError,
			    file_dialog_complete_handler: fileDialogComplete,
			    upload_progress_handler: uploadProgress,
			    upload_error_handler: uploadError,
			    upload_success_handler: showImage,
			    upload_complete_handler: uploadComplete,

			    // Button settings
			    button_image_url: "/SWFUpload/images/XPButtonNoText_160x22.png",
			    button_placeholder_id: "spanButtonPlaceholder",
			    button_width: 160,
			    button_height: 22,
			    button_text: '<span class="button">请选择上传图片<span class="buttonSmall">(2 MB Max)</span></span>',
			    button_text_style: '.button { font-family: Helvetica, Arial, sans-serif; font-size: 14pt; } .buttonSmall { font-size: 10pt; }',
			    button_text_top_padding: 1,
			    button_text_left_padding: 5,

			    // Flash Settings
			    flash_url: "/SWFUpload/swfupload.swf",	// Relative to this file
			    flash9_url: "/SWFUpload/swfupload_FP9.swf",	// Relative to this file

			    custom_settings: {
			        upload_target: "divFileProgressContainer"
			    },

			    // Debug Settings
			    debug: false
			});
        }
        //上传成功以后调用该方法
        function showImage(file, serverData) {
            //$("#showPhoto").attr("src", serverData);
            var data = serverData.split(':');
            //将上传成功的图片作为DIV的背景
            //$("#hiddenImageUrl").val(data[0]);//将上传成功的图片路径存储到隐藏域中。
            $("#selectbanner").attr("src", data[0]);
            $('#selectbanner').imgAreaSelect({
                selectionColor: 'blue', x1: 0, y1: 0, x2: 100,
                y2: 100,selectionOpacity: 0.2, onSelectEnd: preview
            });
        }

        function preview(img, selection)
        {

            $('#selectbanner').data('x', selection.x1);

            $('#selectbanner').data('y', selection.y1);

            $('#selectbanner').data('w', selection.width);

            $('#selectbanner').data('h', selection.height);

        }

        $(function () {
            //让DIV可以移动与拖动大小
            //$("#divCut").draggable({ containment: "#divContent", scroll: false }).resizable({
            //    containment: "#divContent"
            //});
            $("#btnCut").click(function () {
                cutPhoto();
            });
        })
        //截取头像
        function cutPhoto() {
            //计算要截取的头像的范围。
            //var y = $("#divCut").offset().top - $("#divContent").offset().top;//纵坐标
            //var x = $("#divCut").offset().left - $("#divContent").offset().left;
            //var width = $("#divCut").width();
            //var heigth = $("#divCut").height();
            var pars = {
                "x": $('#selectbanner').data('x'),

                "y": $('#selectbanner').data('y'),

                "width": $('#selectbanner').data('w'),

                "height": $('#selectbanner').data('h'),
                "action": "cut",
                "imgSrc": $("#selectbanner").attr("src")
                
            };
            $.post("/ashx/upload.ashx", pars, function (data) {
                $("#showPhoto").attr("src",data);
            });

        }

	</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

            <div id="swfu_container" style="margin: 0px 10px;">
                <div>
                    
                    <span id="spanButtonPlaceholder"></span>
                    
                </div>
                
                <div id="divFileProgressContainer" style="height: 75px;"></div>
                
                <div id="thumbnails"></div>
                <input type="button" value="截取图片" id="btnCut" />
                <input type="hidden" id="hiddenImageUrl" />
	        
                <img id="selectbanner"/>
                <img id="showPhoto"/>
            </div>


</asp:Content>
