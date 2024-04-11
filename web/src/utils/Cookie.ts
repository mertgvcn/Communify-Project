export function setCookie(key: string, value: any, expireDate: Date) {
    const date = new Date(expireDate)

    let expires = "expires=" + date
    document.cookie = `${key}=${value}; ${expires};`
} 

export function deleteCookie(key: string) {
    document.cookie = key + '=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;'
}

export function getCookie(key: string) {
    const decodedCookie = decodeURIComponent(document.cookie)
    const cookieArray = decodedCookie.split("; ")
    let result = "";

    cookieArray.forEach(element => {
        if(element.indexOf(key) === 0) {
            result = element.substring(key.length + 1) //+1 for equal sign
        }
    })

    return result;
}


//Customized methods
export function setLastPageOnCookies(value: any) {
    const date = new Date()
    date.setTime(date.getTime() + (0.5 * 24 * 60 * 60 * 1000)) //half day
    
    let expires = "expires=" + date.toUTCString()
    document.cookie = `lastPage=${value}; ${expires};`
} 