import { atom } from 'jotai';


interface Product {
    discontinued: boolean;
    id: number;
    name:  string
    price: number;
    stock: number;
    count: number;
}
// Atom to manage Customer form state
export const CartAtoms = atom<Product[]>([]);