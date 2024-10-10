export const createOrder = async (orderData: {
    // orderDate: string;
    // deliveryDate: string;
    totalAmount: number;
    customerId: number;
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

        console.log('Create Order Response:', response);

        if (response.ok) {
            const contentType = response.headers.get('content-type');
            if (contentType && contentType.includes('application/json')) {
                return await response.json();
            } else {
                return {};
            }
        } else {
            console.error('Failed to create order:', await response.text());
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

        console.log('Create OrderEntry Response:', response);
        const text = await response.text();
        console.log('Response text:', text);

        if (response.ok) {
            if (text) {
                return JSON.parse(text);
            } else {
                return {};
            }
        } else {
            console.error('Failed to create order entry:', text);
            throw new Error('Failed to create order entry');
        }
    } catch (error) {
        console.error('Error during createOrderEntry:', error);
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
export const getAllOrders = async () => {
    const response = await fetch('http://localhost:5183/api/Order?limit=25&startAt=0', {
        method: "GET",
        credentials: "same-origin",
        headers: {
            "Content-Type": "application/json",
        },
    });
    return await response.json();
};

export const updateOrderStatus = async (orderId: number, status: string) => {
    const response = await fetch(`http://localhost:5183/api/Order/${orderId}`, {
        method: "PUT",
        credentials: "same-origin",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify({
            status: status
        }),
    });
    if (response.ok) {
        return true;
    } else {
        throw new Error('Failed to update order status');
    }
};