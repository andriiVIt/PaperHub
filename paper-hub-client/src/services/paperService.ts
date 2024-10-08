// paperService.ts
export const getPaperList = async () => {
    const response = await fetch('http://localhost:5183/api/Paper?limit=10&startAt=0', {
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
                return {}; // Якщо відповідь не містить JSON
            }
        } else {
            throw new Error('Failed to create paper');
        }
    } catch (error) {
        console.error('Error creating paper:', error);
        throw error;
    }
};

export const updateProductStock = async (productId: number, stock: number) => {
    const response = await fetch(`http://localhost:5183/api/Paper/${productId}`, {
        method: "PUT",
        credentials: "same-origin",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify({
            stock: stock
        }),
    });
    return await response.json();
};

