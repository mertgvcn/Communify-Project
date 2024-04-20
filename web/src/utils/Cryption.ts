import CryptoJS from 'crypto-js'

const keyValue: any = process.env.REACT_APP_ENCRYPTION_KEY
const privateKeyValue: any = process.env.REACT_APP_ENCRYPTION_PRIVATE_KEY

export async function Encrypt(data: string): Promise<string> {
  const _key = CryptoJS.enc.Utf8.parse(keyValue);
  const privatekey = CryptoJS.enc.Utf8.parse(privateKeyValue);
  const encryptedData = CryptoJS.DES.encrypt(data, _key, { iv: privatekey }).toString();

  return encryptedData;
}

export async function Decrypt(encryptedData: any): Promise<string> {
  const _key = CryptoJS.enc.Utf8.parse(keyValue);
  const privateKey = CryptoJS.enc.Utf8.parse(privateKeyValue);
  const decryptedData = CryptoJS.DES.decrypt(encryptedData, _key, { iv: privateKey }).toString(CryptoJS.enc.Utf8);

  return decryptedData;
}