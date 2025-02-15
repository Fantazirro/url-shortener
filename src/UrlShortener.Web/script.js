const apiurl = "http://localhost/api";

document.getElementById("year").textContent = new Date().getFullYear();

const shortenBtn = document.getElementById("shorten-btn");
const urlInput = document.getElementById("url-input");
const loader = document.getElementById("loader");
const result = document.getElementById("result");
const shortenedUrl = document.getElementById("shortened-url");
const copyBtn = document.getElementById("copy-btn");

shortenBtn.addEventListener("click", async () => {
    if (!urlInput.value) return;

    result.classList.remove("flex");
    result.classList.add("d-none");
    
    loader.classList.remove("d-none");
    
    try {
        const response = await fetch(apiurl, {
            method: "post",
            headers: {
                'Accept': 'application/json, text/plain',
                'Content-Type': 'application/json;charset=UTF-8'
            },
            body: JSON.stringify({ url: urlInput.value })
        });
        
        if (response.ok) {
            result.classList.remove("d-none");
            result.classList.add("flex");

            const data = await response.json();
            shortenedUrl.value = data;
            result.classList.remove("d-none");
        }

    }
    catch (error) {
        console.error(error);
    }
    finally {
        loader.classList.add("d-none");
    }
});

copyBtn.addEventListener("click", () => {
    navigator.clipboard.writeText(shortenedUrl.value);
});