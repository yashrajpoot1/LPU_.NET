# Yoga Meditation – Academic Note

## Problem Statement

### 4. Yoga Meditation

### Functionalities

In the `MeditationCenter` class, implement the following properties.

---

## MeditationCenter Class – Properties

| Datatype | Property |
| -------- | -------- |
| int      | MemberId |
| int      | Age      |
| double   | Weight   |
| double   | Height   |
| string   | Goal     |
| double   | BMI      |

---

## Program Class – Requirements

In the code template, a static collection is already provided:

```csharp
public static ArrayList memberList;
```

In the `Program` class, implement the below-given methods.

---

## Method Specifications

### 1. AddYogaMember

```csharp
public void AddYogaMember(int memberId, int age, double weight, double height, string goal)
```

**Description:**

* Adds yoga member details into `memberList`.
* Accepts member ID, age, weight, height, and goal as arguments.
* Stores all values in a `MeditationCenter` object.

---

### 2. CalculateBMI

```csharp
public double CalculateBMI(int memberId)
```

**Description:**

* Calculates and returns the BMI of a yoga member using the given member ID.
* BMI formula:

```
BMI = Weight (Kgs) / (Height (In) × Height (In))
```

* Height is converted from inches to meters.
* BMI value is rounded down to **two decimal places**.
* Stores the BMI in the member object.
* If the member ID is not present:

  * Return `0`
  * Print **"MemberId {memberId} is not present"** in the `Main` method.

**Hint:** Use `Math.Floor()`.

---

### 3. CalculateYogaFee

```csharp
public int CalculateYogaFee(int memberId)
```

**Description:**

* Calculates and returns the membership fee based on the member’s goal and BMI.

#### Fee Rules

| Goal        | Condition         | Membership Fee |
| ----------- | ----------------- | -------------- |
| Weight Loss | BMI ≥ 25 and < 30 | 2000           |
| Weight Loss | BMI ≥ 30 and < 35 | 2500           |
| Weight Loss | BMI ≥ 35          | 3000           |
| Weight Gain | Any BMI           | 2500           |

---

## Main Method Instructions

1. Add yoga members.
2. Calculate BMI using member ID.
3. Display BMI and membership fee.
4. If the member ID is invalid, display an error message.
5. In sample input/output, **bold text** represents user input.

---

## C# Implementation (As Written)

> ⚠️ **Note:**
> The following code is included **exactly as written by the student**.
> No changes were made to logic, comments, structure, or indentation.

```csharp
using System;
using System.Collections;

namespace Learning_Thread
{
    public class Program
    {
        public static ArrayList memberList = new ArrayList();

        public static void Main(string[] args)
        {
            // height is in INCHES, weight is in KG (as per question)
            AddYogaMember(101, 52, 105, 63, "Weight Loss");
            AddYogaMember(102, 55, 50, 65, "Weight Gain");
            // AddYogaMember(103, 25, 75, 63, "Weight Loss");

            double bmi = CalculateBMI(101);

            if (bmi == 0)
            {
                Console.WriteLine("MemberId 101 is not present");
            }
            else
            {
                Console.WriteLine($"Your BMI is {bmi}");
                Console.WriteLine($"Your Membership fee will be: {CalculateYogaFee(101)}");
            }
        }

        /// <summary>
        /// Adds a yoga member to the member list with their details.
        /// </summary>
        public static void AddYogaMember(int memberid, int age, double weight, double height, string goal)
        {
            memberList.Add(new MeditationCenter
            {
                MemberId = memberid,
                Age = age,
                Weight = weight,   // KG
                Height = height,   // INCHES
                Goal = goal
            });
        }

        /// <summary>
        /// Calculates and returns the BMI for a member based on their ID.
        /// </summary>
        public static double CalculateBMI(int memberid)
        {
            foreach (object obj in memberList)
            {
                MeditationCenter member = (MeditationCenter)obj;

                if (member.MemberId == memberid)
                {
                    // Convert height from inches to meters
                    double heightInMeters = member.Height * 0.0254;

                    // BMI = weight(kg) / (height(m) * height(m))
                    double bmi = member.Weight / (heightInMeters * heightInMeters);

                    // Round down to 2 decimal places
                    bmi = Math.Floor(bmi * 100) / 100;

                    // Store BMI in the object
                    member.BMI = bmi;

                    return bmi;
                }
            }
            return 0; // member not found
        }

        /// <summary>
        /// Calculates and returns the membership fee based on goal and BMI.
        /// </summary>
        public static int CalculateYogaFee(int memberid)
        {
            foreach (object obj in memberList)
            {
                MeditationCenter member = (MeditationCenter)obj;

                if (member.MemberId == memberid)
                {
                    double bmi = member.BMI;

                    if (member.Goal == "Weight Loss")
                    {
                        if (bmi >= 35)
                            return 3000;
                        else if (bmi >= 30)
                            return 2500;
                        else if (bmi >= 25)
                            return 2000;
                        else
                            return 1500;
                    }
                    else if (member.Goal == "Weight Gain")
                    {
                        return 2500;
                    }
                }
            }
            return 0;
        }
    }

    public class MeditationCenter
    {
        public int MemberId { get; set; }
        public int Age { get; set; }
        public double Weight { get; set; }  // KG
        public double Height { get; set; }  // INCHES
        public string Goal { get; set; }
        public double BMI { get; set; }
    }
}
```

---

## Conclusion

This program demonstrates:

* Use of `ArrayList` for object storage
* Object-oriented modeling using `MeditationCenter`
* BMI calculation with unit conversion
* Conditional logic for membership fee calculation
* Clean separation of responsibilities using methods
