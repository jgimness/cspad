var uservoiceOptions = {
    /* required */
    key: 'cspad',
    host: 'cspad.uservoice.com',
    forum: '70573',
    showTab: true,
    /* optional */
    alignment: 'left',
    background_color: '#f00',
    text_color: 'white',
    hover_color: '#06C',
    lang: 'en'
};

function _loadUserVoice() {
    var s = document.createElement('script');
    s.setAttribute('type', 'text/javascript');
    s.setAttribute('src', ("https:" == document.location.protocol ? "https://" : "http://") + "cdn.uservoice.com/javascripts/widgets/tab.js");
    document.getElementsByTagName('head')[0].appendChild(s);
}

$(function() {

    function onCodeChange() {
        $('#save-btn').addClass('highlighted');
    }
    var editor = CodeMirror.fromTextArea("code", {
        parserfile: ["csharp.merge"],
        path: "/Scripts/",
        stylesheet: "/Scripts/CodeMirror/csharp/css/csharpcolors.css",
        width: "100%",
        height: "100%",
        tabMode: "shift",
        //lineNumbers: true,
        textWrapping: false,
        initCallback: function(editor) {
            editor.focus();
        },
        onChange: onCodeChange
    });

    $("#save-btn").click(function() {
        if (!$(this).hasClass('highlighted')) { return; }

        $(this).removeClass('highlighted');

        cspad.rpc.save(editor.getCode(), {
            onSuccess: function(result) {
                $('#lastSaved').html("saved at " + (new Date()).toLocaleTimeString());
            },
            onError: function(result) {
                $('#lastSaved').html("save failed");
            }
        });
    });

    var _cloned = false;
    $('#clone-btn').click(function() {
        if (_cloned) { return; }
        _cloned = true;

        cspad.rpc.clone(editor.getCode(), {
            onSuccess: function(result) {
                location = "/" + result;
            }
        });
    });

    $("#recess").click(function() {
        $(this).children("input").select();
    });

    // User voice
    _loadUserVoice();
});
