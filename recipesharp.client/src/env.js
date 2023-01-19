export const dev = window.location.origin.includes('localhost')

// NOTE do not forget to add the s to https when running into Auth errors
export const baseURL = dev ? 'https://localhost:7045' : ''
export const useSockets = false
export const domain = 'dev-esg3qw0wv4oc3r11.us.auth0.com'
export const clientId = 'hulGXtuEYnFl2eBkOovdhEdr7rwSZ9J1'
export const audience = 'https://classroom.com'
