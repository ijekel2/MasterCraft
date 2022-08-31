import { isSupported, setup } from "@loomhq/record-sdk";
import { oembed } from "@loomhq/loom-embed";

window.LoomService = new function () {
    this.init = async function (token, buttonId, dotNetObject, submitText, recordButtonColor, recordButtonHoverColor,
        recordButtonActiveColor, primaryColor, primaryHoverColor, primaryActiveColor) {

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
            jws: token,
            config: {
                insertButtonText: submitText,
                styles: {
                    recordButtonColor: recordButtonColor,
                    recordButtonHoverColor: recordButtonHoverColor,
                    recordButtonActiveColor: recordButtonActiveColor,
                    primaryColor: primaryColor,
                    primaryHoverColor: primaryHoverColor,
                    primaryActiveColor: primaryActiveColor
                }
            }
        });

        const sdkButton = configureButton({ element: button });

        sdkButton.on("insert-click", async (video) => {
            const { html } = this.getVideoEmbedHtml(video.sharedUrl, 800);
            dotNetObject.invokeMethodAsync('OnInsertClick', video, html)
        });
    }

    this.getVideoEmbedHtml = async function (videoSharedUrl, width) {
        return await oembed(videoSharedUrl, { width: width })
    }
}
