function InjectScriptSourceFrame(frameId, scriptUrl) {
    var frame = document.getElementById(frameId);
    console.log("Injecting script into iframe");
    console.log("Script URL:", scriptUrl);

    if (frame) {
        console.log("Iframe found with ID:", frameId);

        // Verifique se o iframe está carregando um documento da mesma origem
        frame.onload = function () {
            try {
                var frameDoc = frame.contentWindow.document;
                var script = document.createElement('script');
                script.src = scriptUrl;
                script.type = 'text/javascript';
                frameDoc.head.appendChild(script);
                console.log("Script injected successfully");
            } catch (e) {
                console.error("Failed to inject script. Possible cross-origin issue:", e);
            }
        };
    } else {
        console.error("Iframe not found with ID:", frameId);
    }
}