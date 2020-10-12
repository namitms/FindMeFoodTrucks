package FindMeFoodTrucks

import (
    "log"
    "io/ioutil"
    "net/http"
)

func search() {
    req, err := http.NewRequest(
        http.MethodGet,
        "https://findmefoodtrucks-apim.azure-api.net/api/FoodTruck?radius=400&longitude=-122.39772&latitude=37.787539&searchString=fried",
        nil,
    )
    if err != nil {
        log.Fatalf("error creating HTTP request: %v", err)
    }

    req.Header.Add("Accept", "application/json")
    req.Header.Add("Ocp-Apim-Subscription-Key", "ba4a272dd98b4a1da72f6bb12727be97")

    res, err := http.DefaultClient.Do(req)
    if err != nil {
        log.Fatalf("error sending HTTP request: %v", err)
    }
    responseBytes, err := ioutil.ReadAll(res.Body)
    if err != nil {
        log.Fatalf("error reading HTTP response body: %v", err)
    }

    log.Println("We got the response:", string(responseBytes))
}