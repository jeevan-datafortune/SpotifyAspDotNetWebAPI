export const GlobalVariables=Object.freeze({
    BASE_API_URL: 'http://localhost:5007/api',
    USER_ID:parseInt( localStorage.getItem('userId') ||'0'),
    USER_NAME:localStorage.getItem('userName') ||'',
    AUTH_TOKEN:  localStorage.getItem('token')||'',
    REFRESH_TOKEN:localStorage.getItem('refreshToken')||'',
    EXPIRES_IN:new Date(localStorage.getItem('expiresIn')||new Date()),
    IsLoggedIn(){
        return this.EXPIRES_IN>=new Date()
    }  
});