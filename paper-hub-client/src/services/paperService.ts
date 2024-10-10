// paperService.ts
export const getPaperList = async () => {
    const response = await fetch('http://localhost:5183/api/Paper?limit=25&startAt=0', {
        method: "GET",
        credentials: "same-origin",
        headers: {
            "Content-Type": "application/json",
        },
    });
    return await response.json();
};

export const createPaper = async (paperData: {
    name: string;
    price: number;
    discontinued: boolean;
    paperProperties: { propertyId: number }[];
    stock: number;
}) => {
    try {
        const response = await fetch('http://localhost:5183/api/Paper', {
            method: 'POST',
            credentials: 'same-origin',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(paperData),
        });

        if (response.ok) {
            const contentType = response.headers.get('content-type');
            if (contentType && contentType.includes('application/json')) {
                return await response.json();
            } else {
                return {};
            }
        } else {
            throw new Error('Failed to create paper');
        }
    } catch (error) {
        console.error('Error creating paper:', error);
        throw error;
    }
};

interface Product {
    id: number;
    name: string;
    price: number;
    stock: number;
    discontinued: boolean;
}

export const updateProductStock = async (product: Product) => {
    const response = await fetch(`http://localhost:5183/api/Paper/${product.id}`, {
        method: "PUT",
        credentials: "same-origin",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify({
            name: product.name,
            price: product.price,
            discontinued: product.discontinued,
            stock: product.stock,
        }),
    });
    // return await response.json();\
    if (response.ok) {
        return true
    } else {
        throw new Error('Failed to update paper stock');
    }
};

