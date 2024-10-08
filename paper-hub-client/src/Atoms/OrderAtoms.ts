import {atom} from "jotai";

interface Order {
    order_date: string;
    delivery_date: string;
    status: string;
    total_amount: number;
    customer_id: number;
    orderEntries: {
        quantity: number;
        productId: number;
    }[]; // Масив товарів
}



// Створюємо атом для збереження ордера
export const orderAtom = atom<Order>({
    order_date: new Date().toISOString(),
    delivery_date: new Date().toISOString(),
    status: 'Pending',
    total_amount: 0,
    customer_id: 0,
    orderEntries: [], // Масив товарів для замовлення
});