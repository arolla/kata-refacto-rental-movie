class Movie:
    CHILDRENS = 2
    NEW_RELEASE = 1
    REGULAR = 0

    def __init__(self, title, *, price_code):
        self.title = title
        self.price_code = price_code
