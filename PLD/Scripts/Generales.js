function BlockBody() {
    $('#divRenderBody').block({
        message: '<h1><i class="fa fa-spinner fa-pulse fa-lg fa-fw"></i></h1>',
        showOverlay: true,
        centerY: true,
        css: {
            border: 'none',
            padding: '5px',
            backgroundColor: '#000',
            '-webkit-border-radius': '10px',
            '-moz-border-radius': '10px',
            opacity: .5,
            color: '#fff'
        }
    });
}

function UnBlockBody() {
    $('#divRenderBody').unblock();
}