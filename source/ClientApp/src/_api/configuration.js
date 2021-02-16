export function Config() {
    var obj = require("./config.json");
    var baseUrl = obj.baseUrl
    return {
        baseUrl : baseUrl,
        apiUrl : `${baseUrl}/api`
    }
}
