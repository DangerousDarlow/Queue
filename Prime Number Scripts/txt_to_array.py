# Read a list of line break separated prime numbers from text file and reformat them as a C# array of longs

with open('primes.txt', 'r') as f:
    count = 0
    string = ''

    for line in f:
        if count >= 128:
            break

        count += 1

        string += f'{line.strip()}L, '

    with open('array.txt', 'w') as out:
        out.write(string)
