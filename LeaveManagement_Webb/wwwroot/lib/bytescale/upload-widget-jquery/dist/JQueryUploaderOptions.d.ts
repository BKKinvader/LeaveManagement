import { JQueryUploaderDropzoneOptions } from "./JQueryUploaderDropzoneOptions";
import { UploadWidgetResult } from "@bytescale/upload-widget";
export interface JQueryUploaderOptions {
    dropzone?: true | JQueryUploaderDropzoneOptions;
    onComplete?: (files: UploadWidgetResult[]) => void;
}
