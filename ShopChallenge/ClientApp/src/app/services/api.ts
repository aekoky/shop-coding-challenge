import { HttpHeaders } from "@angular/common/http";

export abstract class Api {
    protected readonly _server: string;
    protected readonly _headers: HttpHeaders;
    protected abstract readonly _serviceName: string;
    protected abstract readonly _endPointsNames: any;

    constructor() {
        this._server = "https://localhost:44393/api";
    }

    getUrl(endPointName: string, queries?: Array<string>, params?: any): string {
        const endPointURI = [this._server, this._serviceName, endPointName, ""].join("/");
        if (queries) {
            return endPointURI + queries.join("/");
        } else
            if (!params) {
                return endPointURI;
            }
        var queryString = Object.keys(params).map((key) => {
            return encodeURIComponent(key) + "=" + encodeURIComponent(params[key])
        }).join("&");
        return endPointURI.slice(0, -1) + "?" + queryString
    }
}
