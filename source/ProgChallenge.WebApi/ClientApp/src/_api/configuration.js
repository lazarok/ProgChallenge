export function Config() {
    const port = ':44341'
    const baseUrl = `https://${location.hostname}${port}`
    return {
        baseUrl : baseUrl,
        apiUrl : `${baseUrl}/api`
    }
}
