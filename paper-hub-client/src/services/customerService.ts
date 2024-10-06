// import {api} from "../../utils/config.ts";


export const getCustomerList = async () => {
    const response = await fetch('http://localhost:5183/api/Customer?limit=10&startAt=0', {
        method: "GET", // *GET, POST, PUT, DELETE, etc.
        credentials: "same-origin", // include, *same-origin, omit
        headers: {
            "Content-Type": "application/json",
            // 'Content-Type': 'application/x-www-form-urlencoded',
        },
    });
    const data =  await response.json();
    // const response = await api.post(`/Customer?limit=10&startAt=0`)
    //
    // if (response.status !== 200)
    //     throw new Error(response.data && (response.data.Description || response.data.Title || ''))

    console.log(data)

    return data
}
export const createCustomer = async (customerData: { name: string; email: string; phone: string; address: string }) => {
    const response = await fetch('http://localhost:5183/api/Customer', {
        method: 'POST',
        credentials: 'same-origin',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(customerData),
    });

    // Намагайтеся обробити JSON тільки якщо відповідь містить вміст
    if (response.ok) {
        const contentType = response.headers.get('content-type');
        if (contentType && contentType.includes('application/json')) {
            return await response.json();
        } else {
            return {}; // Поверніть порожній об'єкт, якщо немає JSON
        }
    } else {
        throw new Error('Не вдалося створити клієнта');
    }
};