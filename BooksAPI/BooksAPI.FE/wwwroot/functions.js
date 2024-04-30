function addCookie(token, refreshToken){
    document.cookie = `token = ${token}`;
    document.cookie = `refreshToken = ${refreshToken}`;
}

function deleteCookie(token, refreshToken){
    document.cookie = `token=${token};Path=/; Expires=Thu, 01 Jan 1970 00:00:01 GMT;`;
    document.cookie = `refreshToken=${refreshToken};Path=/; Expires=Thu, 01 Jan 1970 00:00:01 GMT;`;
}

function getTokens() {
    console.log(document.cookie);
    let cookies = document.cookie.split(";");
    let token = cookies[0].split("token=")[1];
    let refreshToken = cookies[1].split("refreshToken=")[1];

    return [token, refreshToken];
}