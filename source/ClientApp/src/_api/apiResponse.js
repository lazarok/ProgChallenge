export default class ApiResponse {
    constructor() {
        this.data = param.data;
        this.message = param.message;
        this.succeeded = param.succeeded;
        this.errors = param.errors;
        this.pageNumber = pageNumber;
        this.pageSize = pageSize;
    }
}