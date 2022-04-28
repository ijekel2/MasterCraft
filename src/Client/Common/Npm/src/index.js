import { isSupported, setup } from "@loomhq/record-sdk";
import { oembed } from "@loomhq/loom-embed";

window.LoomService = new function () {
    this.init = async function (token, buttonId, dotNetObject) {
        const { supported, error } = await isSupported();

        if (!supported) {
            console.warn(`Error setting up Loom: ${error}`);
            return;
        }

        const button = document.getElementById(buttonId);

        if (!button) {
            return;
        }

        const { configureButton } = await setup({
            jws: token
        });

        const sdkButton = configureButton({ element: button });

        sdkButton.on("insert-click", async (video) => {
            const { html } = await oembed(video.sharedUrl, { width: 800 });
            dotNetObject.invokeMethodAsync('InsertVideoHtml', html)
        });
    }
}
