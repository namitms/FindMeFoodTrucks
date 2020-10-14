package main

import (
    "log"
    "FindMeFoodTrucks/FindMeFoodTrucks"
)

func main() {
    log.Println("Searching for food trucks ....")
    var b = FindMeFoodTrucks.Search(400,-122.39772,37.787539,"fried","ba4a272dd98b4a1da72f6bb12727be97")
    log.Println("** Results **")
    log.Println(b)
}