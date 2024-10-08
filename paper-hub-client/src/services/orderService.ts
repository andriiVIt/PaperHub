export const createOrder = async (orderData: {
    order_date: string;
    delivery_date: string;
    total_amount: number;
    customer_id: number;
    orderEntries: { quantity: number; productId: number }[];
    status: string;
}) => {
    try {
        const response = await fetch('http://localhost:5183/api/Order', {
            method: 'POST',
            credentials: 'same-origin',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(orderData),
        });

        console.log('Create Order Response:', response); // Логування відповіді

        if (response.ok) {
            const contentType = response.headers.get('content-type');
            if (contentType && contentType.includes('application/json')) {
                return await response.json();
            } else {
                return {}; // Якщо відповідь не містить JSON
            }
        } else {
            console.error('Failed to create order:', await response.text()); // Логування помилки
            throw new Error('Failed to create order');
        }
    } catch (error) {
        console.error('Error in createOrder:', error);
        throw error;
    }
};

export const createOrderEntry = async (orderEntryData: { orderId: number; productId: number; quantity: number }) => {
    try {
        const response = await fetch('http://localhost:5183/api/OrderEntry', {
            method: 'POST',
            credentials: 'same-origin',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(orderEntryData),
        });

        console.log('Create OrderEntry Response:', response); // Логування відповіді
        const text = await response.text(); // Отримайте текстову відповідь
        console.log('Response text:', text); // Лог текстової відповіді

        if (response.ok) {
            if (text) {
                return JSON.parse(text); // Парсимо текст, якщо він не порожній
            } else {
                return {}; // Якщо відповідь порожня
            }
        } else {
            console.error('Failed to create order entry:', text); // Логування помилки
            throw new Error('Failed to create order entry');
        }
    } catch (error) {
        console.error('Error during createOrderEntry:', error); // Логування будь-якої іншої помилки
        throw error;
    }
};
export const getOrderHistory = async (customerId: number) => {
    const response = await fetch(`http://localhost:5183/api/Order/customer/${customerId}/orders`, {
        method: 'GET',
        credentials: 'same-origin',
        headers: {
            'Content-Type': 'application/json',
        },
    });

    if (response.ok) {
        return await response.json();
    } else {
        throw new Error('Failed to fetch order history');
    }
};